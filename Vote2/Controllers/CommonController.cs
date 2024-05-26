using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vote2.IService;
using Vote2.Models;
using Vote2.ViewModels;

namespace Vote2.Controllers
{
    public class CommonController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICommonService _iCommon;
        public CommonController(ApplicationDbContext Context, ICommonService ICommon)
        {
            _Context = Context;
            _iCommon = ICommon;
        }
        public async Task<JsonResult> GetDepartementsByFacultyId(Int64 FacultyId)
        {
            var jsonResualtViewModel = await _iCommon.GetddlDepartementsByFacultyId(FacultyId);
            return new JsonResult(jsonResualtViewModel);
        }
        
        public async Task<JsonResult> GetSectionsByDepartementId(Int64 DepartementId)
        {
            var jsonResualtViewModel = await _iCommon.GetddlSectionsByDepartementId(DepartementId);
            return new JsonResult(jsonResualtViewModel);
        }
    }
}
