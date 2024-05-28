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
                var IfExist = await _Context.Users.Where(x => x.Email == vm.Email || x.PhoneNumber == vm.PhoneNumber).FirstOrDefaultAsync();
                if (IfExist != null)
                {
                    jsonResualtViewModel.IsSuccess = false;
                    if (IfExist.Email == vm.Email)
                    {
                        jsonResualtViewModel.Message = "This Email '" + vm.Email + "' is already in use";
                    }

                    if (IfExist.PhoneNumber == vm.PhoneNumber)
                    {
                        jsonResualtViewModel.Message = jsonResualtViewModel.Message +  " This Phone Number '" + vm.PhoneNumber + "' is already in use";
                    }
                    
                    return new JsonResult(jsonResualtViewModel);
                }
                else
                {
                    Users _users = new Users();
                    _users = vm;
                    _users.ModifiedDate = DateTime.Now;
                    _users.CreatedDate = DateTime.Now;
                    _users.CreatedBy = vm.Email;
                    _users.ModifiedBy = vm.Email;
                    _users.UserTypeId = 2;
                    _Context.Users.Add(_users);
                    await _Context.SaveChangesAsync();

                    Userslevels userslevels = new Userslevels();
                    userslevels.UserId = _users.Id;
                    userslevels.LevelId = vm.LevelId;
                    _Context.UserLevels.Add(userslevels);
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
        
        public async Task<JsonResult> SignIn(string Email, string Password)
        {
            try
            {
                JsonResualtViewModel jsonResualtViewModel = new JsonResualtViewModel();
                
                //var value = HttpContext.Session.GetString("LoginMail");
                var IfExist = _Context.Users.Any(x => x.Email == Email && x.Password == Password);
                if (IfExist)
                {
                    HttpContext.Session.SetString("LoginMail", Email);
                    jsonResualtViewModel.IsSuccess = true;
                    
                    return new JsonResult(jsonResualtViewModel);
                }
                else
                {
                    jsonResualtViewModel.IsSuccess = false;
                    jsonResualtViewModel.Message = "Email or Password isn't correct";
                    return new JsonResult(jsonResualtViewModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
