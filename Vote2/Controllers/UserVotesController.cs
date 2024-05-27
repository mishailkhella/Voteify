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
            
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(VoteInfoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VoteInfo _Vote = new VoteInfo();

                    if (vm.Id > 0)
                    {
                        _Vote = await _Context.Votes.FindAsync(vm.Id);

                        vm.ModifiedDate = DateTime.Now;
                        vm.ModifiedBy = HttpContext.Session.GetString("LoginMail");

                        _Context.Entry(_Vote).CurrentValues.SetValues(vm);
                        await _Context.SaveChangesAsync();
                        return new JsonResult(_Vote);
                    }
                    else
                    {

                        _Vote.Id = vm.Id;
                        _Vote.VoteName = vm.VoteName;
                        _Vote.DepartmentId = vm.DepartmentId;
                        _Vote.SectionId = vm.SectionId;
                        _Vote.LevelId = vm.LevelId;
                        _Vote.FacultyId = vm.FacultyId;
                        _Vote.IsActive = vm.IsActive;
                        _Vote.StartDate = vm.StartDate;
                        _Vote.EndDate = vm.EndDate;
                        _Vote.CreatedBy = HttpContext.Session.GetString("LoginMail");
                        _Vote.CreatedDate = DateTime.Now;
                        _Context.Votes.Add(_Vote);
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
