using Microsoft.AspNetCore.Mvc;
using SMS.Models;
using SMS.Service;
using SMS.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SMS.Utility;

namespace SMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel _obj)
        {
            if (string.IsNullOrEmpty(_obj.UserId) || string.IsNullOrEmpty(_obj.Password))
            {
                ViewBag.Message = "User ID and Password are required.";
                return View(_obj);
            }

            Tuple<List<User>, string> _tpl = _loginService.GetUserById(_obj.UserId, _obj.Password);
            var users = _tpl.Item1;
            var message = _tpl.Item2;

            bool UserExist = users.Any();
            if (UserExist)
            {
                // Store the user list in the session
                SessionHelper.SetObjectAsJson(HttpContext.Session, "_userInfo", _tpl.Item1);
                HttpContext.Session.SetString("LoggedInUsers", JsonConvert.SerializeObject(users));

                // Using null-coalescing operator to provide a default value in case of null
                HttpContext.Session.SetString("_UserId", _tpl.Item1.Select(c => c.UserId).FirstOrDefault() ?? string.Empty);
                HttpContext.Session.SetString("_Password", _tpl.Item1.Select(c => c.Password).FirstOrDefault() ?? string.Empty);
                HttpContext.Session.SetString("_UserName", _tpl.Item1.Select(c => c.UserName).FirstOrDefault() ?? string.Empty);
                HttpContext.Session.SetString("_Designation", _tpl.Item1.Select(c => c.Designation).FirstOrDefault() ?? string.Empty);

                ViewBag.Message = "Login successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid User Id and Password";
                return View(_obj);
            }
        }

        public IActionResult Logout()
        {
                // Clear the session
                HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }

        public IActionResult Profile()
        {
            // Retrieve the user list from the session
            var userListJson = HttpContext.Session.GetString("LoggedInUsers");
            if (userListJson != null)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(userListJson);
                return View(users);
            }

            return RedirectToAction("Index");
        }
    }
}
