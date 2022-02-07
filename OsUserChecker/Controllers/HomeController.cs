using Microsoft.AspNetCore.Mvc;
using OsUserChecker.Models;
using System.Diagnostics;
using System.Net;
using System.Management;
using System.Text;

namespace OsUserChecker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Cheсker()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Failure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cheсker(string name)
        {
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_UserAccount");
            ManagementObjectCollection users = usersSearcher.Get();
            foreach (ManagementObject user in users)
            {
                if (name == user["Name"].ToString())
                {
                    return Redirect("/Home/Success");
                }
            }
            return Redirect("/Home/Failure");
        }

        [Route("/Home/RedirectToCheсker")]
        public IActionResult RedirectToCheсker()
        {
            var encodedLocationName = WebUtility.UrlEncode("/Cheсker");
            return Redirect(encodedLocationName.Remove(0, 3));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}