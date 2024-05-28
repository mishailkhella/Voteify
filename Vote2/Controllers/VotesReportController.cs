 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vote2.IService;
using Vote2.Models;
using Vote2.ViewModels;
using System.Linq.Dynamic.Core;

namespace Vote2.Controllers
{
    public class VotesReportController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICommonService _iCommon;
        public VotesReportController(ApplicationDbContext Context, ICommonService ICommon)
        {
            _Context = Context;
            _iCommon = ICommon;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetDataTabelData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int resultTotal = 0;

                var _GetGridItem = _iCommon.GetAllVotes();
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {

                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);

                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.StartDate.ToString().ToLower().Contains(searchValue)
                    || obj.EndDate.ToString().ToLower().Contains(searchValue)
                    || obj.VoteName.ToLower().Contains(searchValue)
                    || obj.FacultyName.ToLower().Contains(searchValue)
                    || obj.DepartmentName.ToLower().Contains(searchValue)
                    || obj.SectionName.ToLower().Contains(searchValue)
                    || obj.LevelName.ToLower().Contains(searchValue)

                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToLower().Contains(searchValue)
                    || obj.CreatedDate.ToString().Contains(searchValue)
                    || obj.CreatedBy.ToLower().Contains(searchValue));
                }

                resultTotal = _GetGridItem.Count();
                var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet]
        public async Task<IActionResult> Report(Int64 VoteId)
        {
            VotesReportViewModel viewModel = new VotesReportViewModel();
            viewModel.QuestionsReportList = new();
            var Vote = await _Context.Votes.Where(i => i.Id == VoteId && i.Cancelled == false).FirstOrDefaultAsync();
            if (Vote == null)
            {
                return NoContent();
            }
            var AllUsers = await _Context.Users.Where(i => i.Cancelled == false).ToListAsync();
            var AllVoters = await _Context.VotedUsers.Where(i => i.VotedId == VoteId).ToListAsync();

            viewModel.VoteName = Vote.VoteName;
            viewModel.VoteId = Vote.Id;
            viewModel.VoteEndDate = Vote.EndDate;
            viewModel.NoOfVoters = AllVoters.Count;


            var AllQuestions = await _Context.Questions.Where(i => i.VoteId == VoteId && i.Cancelled == false).ToListAsync();
            var AllAnswers = await _Context.QuestionAnswer.Where(i => i.Cancelled == false && i.VoteId == VoteId).ToListAsync();

            var AllUserAnswersVote = await _Context.UserAnswersVote.Where(i => i.VoteId == VoteId).ToListAsync();

            foreach (var Question in AllQuestions)
            {
                QuestionsReportViewModel voteQuestion = new QuestionsReportViewModel();
                voteQuestion.QuestionsAnswersReportList = new List<QuestionsAnswersReportViewModel>();
                voteQuestion.Id = Question.Id;
                voteQuestion.QuestionName = Question.QuestionName;
                voteQuestion.QuestionTypeId = Question.QuestionTypeId;

                if (Question.QuestionTypeId == 2) //Choice
                {
                    var AllQuestionVotedAnswers = AllUserAnswersVote.Where(i => i.QuestionId == Question.Id).ToList();

                    var QuestionAnswers = AllAnswers.Where(i => i.QuestionId == Question.Id).ToList();
                    foreach (var Answer in QuestionAnswers)
                    {
                        var ThisAnswerVotes = AllQuestionVotedAnswers.Where(i => i.AnswerId == Answer.Id).ToList();
                        QuestionsAnswersReportViewModel questionsAnswersReportViewModel = new QuestionsAnswersReportViewModel();
                        questionsAnswersReportViewModel.Id = Answer.Id;
                        questionsAnswersReportViewModel.AnswerName = Answer.AnswerName;

                        double PercentageAnswer = Math.Round(Convert.ToDouble(ThisAnswerVotes.Count) / Convert.ToDouble(AllQuestionVotedAnswers.Count), 2);

                        questionsAnswersReportViewModel.Percentage = PercentageAnswer * 100;

                        voteQuestion.QuestionsAnswersReportList.Add(questionsAnswersReportViewModel);
                    }
                }
                else if (Question.QuestionTypeId == 1) //Text
                {
                    var QuestionAnswersTexts = AllUserAnswersVote.Where(i => AllVoters.Select(x => x.Id).Contains(i.VotedUserId) && i.QuestionId == Question.Id).ToList();
                    foreach (var QuestionAnswerText in QuestionAnswersTexts)
                    {
                        QuestionsAnswersReportViewModel questionsAnswersReportViewModel = new QuestionsAnswersReportViewModel();
                        questionsAnswersReportViewModel.AnswerText = QuestionAnswerText.AnswerText;
                        var Voter = AllVoters.Where(i => i.Id == QuestionAnswerText.VotedUserId).FirstOrDefault();
                        if (Voter != null)
                        {
                            questionsAnswersReportViewModel.VoteDate = Voter.VoteDate;
                            var User = AllUsers.Where(i => i.Id == Voter.UserId).FirstOrDefault();
                            if (User != null)
                            {
                                questionsAnswersReportViewModel.UserName = User.Name;
                            }
                        }
                        voteQuestion.QuestionsAnswersReportList.Add(questionsAnswersReportViewModel);
                    }
                }
                viewModel.QuestionsReportList.Add(voteQuestion);
            }
            return View(viewModel);
        }
    }
}
