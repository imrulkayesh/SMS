using Microsoft.AspNetCore.Mvc;
using SMS.ViewModels;
using SMS.Service;
using System.Threading.Tasks;
using SMS.Models;
using SMS.Utility;
using Microsoft.EntityFrameworkCore;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            List<User>? _user_info = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, "_userInfo");
            if (_user_info !=null) 
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel _obj, string id)
        {
            //if (ModelState.IsValid)
            if (_obj.UserId != null)
            {
                string result = await _userService.SaveChangeAsync(_obj, true);

                if (result == "Success")
                {
                    TempData["msg"] = "Data saved successfully!";
                }
                else
                {
                    TempData["msg"] = $"Error: {result}";
                }

                return RedirectToAction("Index");
            }

            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors);
            //    foreach (var error in errors)
            //    {
            //        // Log or display the errors
            //        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
            //    }
            //    ModelState.AddModelError("Err", "Invalid Data");
            //    return View(_obj);
            //}

            ModelState.AddModelError("Err", "Invalid Data");
            return View(_obj);
        }
        public IActionResult ChangePassword()
        {
            List<User>? _user_info = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, "_userInfo");
            if (_user_info != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserViewModel _obj)
        {
            List<User>? _user_info = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, "_userInfo");

            var userId = _user_info?.Select(c => c.UserId).FirstOrDefault();
            var userPassword = _user_info?.Select(c => c.Password).FirstOrDefault();

            if (userId == null || userPassword == null)
            {
                TempData["msg"] = ("User not found in session");
                return View(_obj);
            }

            if (_obj.OldPassword == null || _obj.NewPassword == null || _obj.ConfirmPassword == null)
            {
                TempData["msg"] = ("Password fields cannot be null");
                return View(_obj);
            }
            // Verify old password
            if (!await _userService.VerifyPasswordAsync(userId, _obj.OldPassword))
            {
                TempData["msg"] = "Old password is incorrect";
                return View(_obj);
            }

            if (_obj.NewPassword != _obj.ConfirmPassword)
            {
                TempData["msg"] = ("New password and confirm password do not match");
                return View(_obj);
            }

            // Proceed only if model.NewPassword is not null
            var result = await _userService.ChangePasswordAsync(userId, _obj.NewPassword);

            if (result == "Success")
            {
                TempData["msg"] = ("Password change successful");
                return RedirectToAction("ChangePassword");
            }
            else
            {
                TempData["msg"] = result;
                return View(_obj);
            }
            
        }
        public IActionResult PasswordReset()
        {
            List<User>? _user_info = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, "_userInfo");
            if (_user_info != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(UserViewModel _obj)
        {
            if (_obj.UserId == null || _obj.NewPassword == null || _obj.ConfirmPassword == null)
            {
                TempData["msg"] = ("Password fields cannot be null");
                return View(_obj);
            }
            if (_obj.NewPassword != _obj.ConfirmPassword)
            {
                TempData["msg"] = ("New password and confirm password do not match");
                return View(_obj);
            }

            var result = await _userService.ChangePasswordAsync(_obj.UserId, _obj.NewPassword);

            if (result == "Success")
            {
                TempData["msg"] = ("Password change successful");
                return RedirectToAction("PasswordReset");
            }
            else
            {
                TempData["msg"] = result;
                return View(_obj);
            }

        }

        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUserAsync();
            if (result.Item2 == "Success")
            {
                return Json(new { data = result.Item1 });
            }
            else
            {
                TempData["msg"] = result.Item2;
                return Json(new { data = new List<User>() });
            }
        }




    }
}

