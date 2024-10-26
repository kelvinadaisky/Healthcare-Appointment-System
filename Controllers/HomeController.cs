using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_prog_Project.Models;

namespace Web_prog_Project.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Assistants()
        {
            return View();
        }

        public IActionResult Professors()
        {
            return View();
        }

        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult Appointments()
        {
            return View();
        }

        public IActionResult Emergencies()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

    }
}
