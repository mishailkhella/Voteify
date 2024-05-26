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
                var IfExist = _Context.Users.Any(x => x.UserName == vm.UserName && x.Password == vm.Password && x.Name ==vm.Name);
				if (IfExist)
                {
                    jsonResualtViewModel.IsSuccess = false;
                    jsonResualtViewModel.Message = "This Account is Exist";
					return new JsonResult(jsonResualtViewModel);
				}
                else
                {
                    Users _users = new Users();

                    _users.ModifiedDate = DateTime.Now;
                    _users.CreatedDate = DateTime.Now;
                    _users.Email = vm.Email;    
                    _users.Name = vm.Name;
                    _users.Password = vm.Password;

                    _Context.Users.Add(_users);
                    await _Context.SaveChangesAsync();
                    jsonResualtViewModel.IsSuccess = true;


                    return new JsonResult(jsonResualtViewModel);				}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Vote2.ViewModels;

//public class LogInController : Controller
//{

//	private readonly ApplicationDbContext _Context;
//	public LogInController()
//	{
//		_Context = new ApplicationDbContext();
//	}
//	// GET: /Login/SignIn
//	[HttpGet]
//	public IActionResult SignIn()
//	{
//		return View();
//	}

//	// POST: /Login/SignIn
//	[HttpPost]
//	public IActionResult SignIn(string Email, string password)
//	{ // Check if the user exists in the database
//		var user = _Context.Users.FirstOrDefault(u => u.UserEmail == Email && u.Password == password);

//		if (user != null)
//		{
//			// User exists, do something (e.g., redirect to home page)
//			return RedirectToAction("Index", "Home");
//		}
//		else
//		{
//			// User not found, handle registration or display a message
//			// For demonstration purposes, we'll just display a message
//			ViewBag.Message = "User not found. Please register.";
//			return View("SignIn");
//		}
//	}
//}

