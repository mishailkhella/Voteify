using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vote2.IService;
using Vote2.Models;
using Vote2.ViewModels;
using System.Linq.Dynamic.Core;

namespace Vote2.Controllers
{
    public class UserVotesController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICommonService _iCommon;
        public UserVotesController(ApplicationDbContext Context, ICommonService ICommon)
        {
            _Context = Context;
            _iCommon = ICommon;
        }
        public async Task<IActionResult> Index()
        {
            UserVotesViewModel viewModel = new UserVotesViewModel();
            viewModel.VotesViewModelList = new List<VotesViewModel>();

            var Email = HttpContext.Session.GetString("LoginMail");
            if (Email == null)
                return NotFound();

            var user = await _Context.Users.Where(i => i.Email == Email && i.Cancelled == false).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            viewModel.UserId = user.Id;
            viewModel.UserName = user.Name;

            var UserLevel = await _Context.UserLevels.Where(i => i.UserId == user.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            var AllVotes = await _Context.Votes.Where(i => i.Cancelled == false && i.IsActive == true).ToListAsync();

            if (user.FacultyId > 0)
            {
                AllVotes = AllVotes.Where(i => i.FacultyId == user.FacultyId).ToList();
            }
            if (user.DepartmentId > 0)
            {
                AllVotes = AllVotes.Where(i => i.DepartmentId == user.DepartmentId).ToList();
            }
            if (user.SectionId > 0)
            {
                AllVotes = AllVotes.Where(i => i.SectionId == user.SectionId).ToList();
            }

            if (UserLevel != null)
            {
                AllVotes = AllVotes.Where(i => i.LevelId == UserLevel.LevelId).ToList();
            }

            AllVotes = AllVotes.Where(i => i.EndDate >= DateTime.Now && i.StartDate <= DateTime.Now).ToList();

            var AllVotedUsers = await _Context.VotedUsers.Where(i => i.UserId == user.Id).Select(x => x.VotedId).ToListAsync();
            if (AllVotedUsers.Count > 0)
            {
                AllVotes = AllVotes.Where(i => !AllVotedUsers.Contains(i.Id)).ToList();
            }

            viewModel.VotesViewModelList = AllVotes.Select(i => new VotesViewModel()
            {
                Id = i.Id,
                VoteName = i.VoteName,
                EndDate = i.EndDate
            }).ToList();


            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Vote(Int64 VoteId, Int64 UserId)
        {
            UserAnswerVotesViewModel viewModel = new UserAnswerVotesViewModel();
            viewModel.VoteQuestionsList = new();
            var Vote = await _Context.Votes.Where(i => i.Id == VoteId && i.IsActive == true && i.EndDate >= DateTime.Now && i.StartDate <= DateTime.Now && i.Cancelled == false).FirstOrDefaultAsync();
            if (Vote == null)
            {
                return RedirectToAction("Index");
            }
            var User = await _Context.Users.Where(i => i.Id == UserId && i.Cancelled == false).FirstOrDefaultAsync();
            if (User == null)
            {
                return NoContent();
            }
            viewModel.VoteName = Vote.VoteName;
            viewModel.VoteId = Vote.Id;
            viewModel.VoteEndDate = Vote.EndDate;

            viewModel.UserId = User.Id;
            viewModel.UserName = User.Name;

            var AllQuestions = await _Context.Questions.Where(i => i.VoteId == VoteId && i.Cancelled == false).ToListAsync();
            var AllAnswers = await _Context.QuestionAnswer.Where(i => i.Cancelled == false && i.VoteId == VoteId).ToListAsync();
            foreach (var Question in AllQuestions)
            {
                VoteQuestionsViewModel voteQuestion = new VoteQuestionsViewModel();
                voteQuestion.VoteQuestionsAnswersList = new List<VoteQuestionsAnswersViewModel>();
                voteQuestion.Id = Question.Id;
                voteQuestion.QuestionName = Question.QuestionName;
                voteQuestion.QuestionTypeId = Question.QuestionTypeId;

                if (Question.QuestionTypeId == 2) //Choice
                {
                    var QuestionAnswers = AllAnswers.Where(i => i.QuestionId == Question.Id).ToList();

                    voteQuestion.VoteQuestionsAnswersList = QuestionAnswers.Select(i => new VoteQuestionsAnswersViewModel()
                    {
                        AnswerName = i.AnswerName,
                        Id = i.Id
                    }).ToList();
                }
                viewModel.VoteQuestionsList.Add(voteQuestion);
            }
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Vote(UserAnswerVotesViewModel vm)
        {
            try
            {
                if (vm.VoteQuestionsList != null)
                {
                    VotedUsers votedUsers = new VotedUsers();
                    votedUsers.UserId = vm.UserId;
                    votedUsers.VotedId = vm.VoteId;
                    votedUsers.VoteDate = DateTime.Now;

                    _Context.VotedUsers.Add(votedUsers);
                    await _Context.SaveChangesAsync();

                    foreach (var Question in vm.VoteQuestionsList)
                    {
                        UserAnswersVote userAnswers = new UserAnswersVote();
                        userAnswers.AnswerText = Question.AnswerText;
                        userAnswers.AnswerId = Question.SelectedAnswerId;
                        userAnswers.VotedUserId = votedUsers.Id;
                        userAnswers.VoteId = vm.VoteId;
                        userAnswers.QuestionId = Question.Id;

                        _Context.UserAnswersVote.Add(userAnswers);
                        await _Context.SaveChangesAsync();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
                throw ex;
            }

        }

































        //[HttpPost]
        //public IActionResult GetDataTabelData()
        //{
        //    try
        //    {
        //        var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();

        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var sortColumnAscDesc = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();

        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int resultTotal = 0;

        //        var _GetGridItem = _iCommon.GetAllVotes();
        //        //Sorting
        //        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
        //        {

        //            _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);

        //        }
        //        //Search
        //        if (!string.IsNullOrEmpty(searchValue))
        //        {
        //            searchValue = searchValue.ToLower();
        //            _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
        //            || obj.Id.ToString().ToLower().Contains(searchValue)
        //            || obj.LevelId.ToString().ToLower().Contains(searchValue)
        //            || obj.StartDate.ToString().ToLower().Contains(searchValue)
        //            || obj.EndDate.ToString().ToLower().Contains(searchValue)
        //            || obj.SectionId.ToString().ToLower().Contains(searchValue)

        //            || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
        //            || obj.ModifiedBy.ToString().ToLower().Contains(searchValue)
        //            || obj.CreatedDate.ToString().Contains(searchValue)
        //            || obj.CreatedBy.ToLower().Contains(searchValue));
        //        }
        //        resultTotal = _GetGridItem.Count();
        //        var result = _GetGridItem.Skip(skip).Take(pageSize).ToList();
        //        return Json(new { draw = draw, recordsFiltered = resultTotal, recordsTotal = resultTotal, data = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        //[HttpPost]
        //public async Task<JsonResult> Delete(Int64 id)
        //{
        //    JsonResualtViewModel jsonResualtViewModel = new JsonResualtViewModel();
        //    try
        //    {

        //        var _Vote = await _Context.Votes.FindAsync(id);
        //        _Vote.Cancelled = true;
        //        _Context.Update(_Vote);
        //        await _Context.SaveChangesAsync();
        //        jsonResualtViewModel.IsSuccess = true;
        //        jsonResualtViewModel.Message = "Question Deleted Successfully. ID: " + _Vote.Id;
        //        return new JsonResult(jsonResualtViewModel);

        //    }
        //    catch (Exception ex)
        //    {

        //        jsonResualtViewModel.IsSuccess = false;
        //        jsonResualtViewModel.Message = ex.Message;
        //        return new JsonResult(jsonResualtViewModel); 
        //        throw ex;
        //    }


        //}

    }
}
