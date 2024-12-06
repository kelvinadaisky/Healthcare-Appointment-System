using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Data;
using Web_prog_Project.Models;

namespace Web_prog_Project.Controllers
{
    public class AssistantShiftController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssistantShiftController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shift/Create
        public IActionResult Create()
        {
            ViewData["Assistants"] = _context.Assistants.Select(a => new SelectListItem
            {
                Value = a.AssistantId.ToString(),
                Text = a.FirstName,
            }).ToList();

            ViewData["Departments"] = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();
            return View();
        }

        // POST: Shift/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssistantShift shift)
        {
            if (ModelState.IsValid)
            {
                _context.AssistantShifts.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction("Schedule");
            }
            ViewData["Assistants"] = _context.Assistants.Select(a => new SelectListItem
            {
                Value = a.AssistantId.ToString(),
                Text = a.FirstName,
            }).ToList();

            ViewData["Departments"] = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();
            return View(shift);
        }

        public async Task<IActionResult> Schedule()
        {
            //var shifts = await _context.AssistantShifts.Include(s => s.AssistantId).ToListAsync();
            return View(/*shifts*/);
        }
    }
}
