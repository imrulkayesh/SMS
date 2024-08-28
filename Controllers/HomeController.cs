using Microsoft.AspNetCore.Mvc;
using SMS.Data;
using SMS.Models;
using SMS.Utility;
using System.Diagnostics;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SMSDBContext _context;

        public HomeController(ILogger<HomeController> logger, SMSDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<User>? _user_info = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, "_userInfo");
            //var userId = _user_info.Select(c => c.UserId).FirstOrDefault();
            //bool Userdata = userId.Any();
            if (_user_info != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult Privacy()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
