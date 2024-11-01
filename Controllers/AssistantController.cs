using Microsoft.AspNetCore.Mvc;
using Web_prog_Project.Data;
using Web_prog_Project.Models;

namespace Web_prog_Project.Controllers
{
    public class AssistantController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AssistantController(ApplicationDbContext db)
        { 
            _db = db;
        }
        public IActionResult Index()
        {
            List<Assistant> objAssistantList = _db.Assistants.ToList();
            return View(objAssistantList);
        }
    }
}
