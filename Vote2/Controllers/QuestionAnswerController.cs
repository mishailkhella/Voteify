using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vote2.IService;
using Vote2.Models;
using Vote2.ViewModels;
using System.Linq.Dynamic.Core;

namespace Vote2.Controllers
{
    public class QuestionAnswerController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICommonService _iCommon;
        public QuestionAnswerController(ApplicationDbContext Context, ICommonService ICommon)
        {
            _Context = Context;
            _iCommon = ICommon;
        }
        public IActionResult Index()
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

                var _GetGridItem = _iCommon.GetAllQuestionAnswer();
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnAscDesc)))
                {

                    _GetGridItem = _GetGridItem.OrderBy(sortColumn + " " + sortColumnAscDesc);
                     
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    searchValue = searchValue.ToLower();
                    _GetGridItem = _GetGridItem.Where(obj => obj.Id.ToString().Contains(searchValue)
                    || obj.Id.ToString().ToLower().Contains(searchValue)
                    || obj.AnswerName.ToLower().Contains(searchValue)
                    || obj.VoteName.ToLower().Contains(searchValue)
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
        public async Task<IActionResult> AddEdit(int id)
        {
            QuestionAnswerViewModel vm = new();
            var votes = _Context.Votes.Where(x=>x.Cancelled==false).ToList();
            ViewBag.GetddlVotes = new SelectList(votes, "Id", "VoteName");
        
            if (id > 0)
            {
                QuestionAnswer _QuestionAnswer = await _Context.QuestionAnswer.Where(x => x.Cancelled == false && x.Id == id).FirstOrDefaultAsync();
                if (_QuestionAnswer == null)
                {
                    return NotFound();
                }
                vm = _QuestionAnswer;

                var Question = _Context.Questions.Where(z => z.Cancelled == false && z.QuestionTypeId == 2 && z.VoteId == _QuestionAnswer.VoteId).ToList();
                ViewBag.GetddlQuestions = new SelectList(Question, "Id", "QuestionName");

                return PartialView("_AddEdit", vm);

                //vm = await _Context.QuestionAnswer.FindAsync(vm.Id);

                //return PartialView("_AddEdit", vm);
            }
            else
            {
                return PartialView("_AddEdit", vm);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(QuestionAnswerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    QuestionAnswer _questionAnswer = new ();

                    if (vm.Id > 0)
                    {
                        _questionAnswer = await _Context.QuestionAnswer.FindAsync(vm.Id);

                      
                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy =  HttpContext.Session.GetString("LoginMail");
                        _Context.Entry(_questionAnswer).CurrentValues.SetValues(vm);
                        await _Context.SaveChangesAsync();
                        return new JsonResult(_questionAnswer);
                    }
                    else
                    {

                        _questionAnswer.Id = vm.Id;
                        _questionAnswer.AnswerName = vm.AnswerName;
                        _questionAnswer.QuestionId = vm.QuestionId;
                        _questionAnswer.VoteId = vm.VoteId;
                        _questionAnswer.CreatedBy = HttpContext.Session.GetString("LoginMail");
                        _questionAnswer.ModifiedBy = HttpContext.Session.GetString("LoginMail");
                        _questionAnswer.CreatedDate = DateTime.Now;
                        _questionAnswer.ModifiedDate = DateTime.Now;
                        _Context.QuestionAnswer.Add(_questionAnswer);
                        _Context.SaveChanges();
                        return new JsonResult(vm);
                    }
                }
                catch (Exception ex)
                {
                    return new JsonResult(ex.Message);
                    throw ex;
                }
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(Int64 id)
        {
            JsonResualtViewModel jsonResualtViewModel = new JsonResualtViewModel();
            try
            {

                var _question = await _Context.Questions.FindAsync(id);
                _question.Cancelled = true;
                _Context.Update(_question);
                await _Context.SaveChangesAsync();
                jsonResualtViewModel.IsSuccess = true;
                jsonResualtViewModel.Message = "Question Deleted Successfully. ID: " + _question.Id;
                return new JsonResult(jsonResualtViewModel);

            }
            catch (Exception ex)
            {

                jsonResualtViewModel.IsSuccess = false;
                jsonResualtViewModel.Message = ex.Message;
                return new JsonResult(jsonResualtViewModel); 
                throw ex;
            }

          
        }

    }
}
