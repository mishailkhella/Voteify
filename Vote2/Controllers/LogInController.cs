using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vote2.Models;
using Vote2.ViewModels;

namespace Vote2.Controllers
{
    public class LogInController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public LogInController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            UsersViewModel vm = new UsersViewModel();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UsersViewModel vm)
        {
            try
            {
                var IfExist = _Context.Users.Any(x => x.UserName == vm.UserName && x.Password == vm.Password && x.Name ==vm.Name);

				if (IfExist)
                {
                    ViewBag.Notification = "This Account is Exist";
					return RedirectToAction("Index", "LogIn");
				}
                else
                {
                    Users _users = new Users();
                    _Context.Users.Add(vm);
                    await _Context.SaveChangesAsync();
                    vm.UserEmail = _users.Email;
                    vm.UserName = _users.UserName;
                    vm.PhoneNumber = _users.PhoneNumber;
                    vm.Name = _users.Name;
                    vm.Password = _users.Password;

					return RedirectToAction("Index", "LogIn");
				}
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

