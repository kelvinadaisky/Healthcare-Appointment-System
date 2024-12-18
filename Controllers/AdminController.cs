using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_prog_Project.Data;

namespace Web_prog_Project.Controllers
{
    [Authorize(Roles = Web_prog_Project.Utility.Helper.Admin)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
