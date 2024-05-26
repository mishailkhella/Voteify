using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vote2.IService;
using Vote2.Models;
using Vote2.ViewModels;

namespace Vote2.Controllers
{
    public class LogInController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly ICommonService _iCommon;
        public LogInController(ApplicationDbContext Context, ICommonService ICommon)
        {
            _Context = Context;
            _iCommon = ICommon;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            UsersViewModel vm = new UsersViewModel();
            ViewBag.DDLFaculties = new SelectList(_iCommon.GetddlFaculties(), "Id", "Name");
            ViewBag.DDLLevels = new SelectList(_iCommon.GetddlLevels(), "Id", "Name");
            return View(vm);
        }
        [HttpPost]
        public async Task<JsonResult> SignUp(UsersViewModel vm)
        {
            try
            {
                JsonResualtViewModel jsonResualtViewModel = new JsonResualtViewModel();
                //HttpContext.Session.SetString("LoginMail", vm.Email);
                //var value = HttpContext.Session.GetString("LoginMail");
                var IfExist = _Context.Users.Any(x => x.Email == vm.Email && x.Password == vm.Password && x.Name == vm.Name);
                if (IfExist)
                {
                    jsonResualtViewModel.IsSuccess = false;
                    jsonResualtViewModel.Message = "This Account is Exist";
                    return new JsonResult(jsonResualtViewModel);
                }
                else
                {
                    Users _users = new Users();
                    _users = vm;
                    _users.ModifiedDate = DateTime.Now;
                    _users.CreatedDate = DateTime.Now;

                    _Context.Users.Add(_users);
                    await _Context.SaveChangesAsync();
                    jsonResualtViewModel.IsSuccess = true;


                    return new JsonResult(jsonResualtViewModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
