using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vote2.IService;
using Vote2.Models;
using Vote2.ViewModels;
using System.Linq.Dynamic.Core;


namespace Vote2.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICommonService _iCommon;
        public QuestionController(ApplicationDbContext Context, ICommonService ICommon)
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

                var _GetGridItem = _iCommon.GetAllQuestion();
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
                    || obj.QuestionName.ToLower().Contains(searchValue)
                    || obj.QuestionId.ToString().ToLower().Contains(searchValue)
                    || obj.QuestionTypeId.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedDate.ToString().ToLower().Contains(searchValue)
                    || obj.ModifiedBy.ToString().ToLower().Contains(searchValue)
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
            QuestionViewModel vm = new();
            var votes = _Context.Votes.ToList();
            ViewBag.GetddlVotes = new SelectList(votes, "Id", "VoteName");

            var Type =_Context.Questionstype.ToList();
            ViewBag.GetddlQuestionType = new SelectList(Type, "Id", "QuestionTypeName");
            //ViewBag.Votes = new SelectList(await _iCommon.GetddlQuestionsByVoteId(vm.Id), "Id", "VoteName");
            if (id > 0)
            {
                Question _Question = await _Context.Questions.Where(x => x.Cancelled == false).FirstOrDefaultAsync();
                if (_Question == null)
                {
                    return NotFound();
                }
                vm = _Question;
                ViewBag.Question = new SelectList(await _iCommon.GetddlQuestionsByVoteId(_Question.Id), "Id", "QuestionTypeName");
                //ViewBag.Votes = new SelectList( _iCommon.GetAllVotes(), "Id", "VoteName");
                return PartialView("_AddEdit", vm);
            }
            else
            {
                return PartialView("_AddEdit", vm);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(QuestionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Question _question = new ();

                    if (vm.Id > 0)
                    {

                        _question = await _Context.Questions.FindAsync(vm.Id);
                        vm.CreatedDate = _question.CreatedDate;
                        vm.CreatedBy = _question.CreatedBy;
                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy =  HttpContext.Session.GetString("LoginMail");
                        _Context.Entry(_question).CurrentValues.SetValues(vm);
                        await _Context.SaveChangesAsync();
                        return new JsonResult(_question);
                    }
                    else
                    {

                        _question.Id = vm.Id;
                        _question.QuestionName = vm.QuestionName;
                        _question.QuestionTypeId = vm.QuestionTypeId;
                        _question.VoteId = vm.VoteId;
                        _question.CreatedBy = HttpContext.Session.GetString("LoginMail");
                        _question.CreatedDate = DateTime.Now;
                        _question.ModifiedBy = HttpContext.Session.GetString("LoginMail");
                        _question.ModifiedDate = DateTime.Now;
                        _Context.Questions.Add(_question);
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
