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
            // Fetch the assistant shifts, including the assistant's name and shift date
            var shifts = await _context.AssistantShifts
                .Include(s => s.Assistant)  // Include the Assistant entity
                .ToListAsync();

            // Prepare the events for the calendar
            var events = shifts.Select(s => new
            {
                title = $"Asistan Nöbeti: {s.Assistant.FirstName} {s.Assistant.LastName}",  // Title with assistant's full name
                start = s.StartTime.ToString("yyyy-MM-dd")  // Format the date to fit the calendar event format
            }).ToList();

            // Pass the events to the view
            ViewData["Shifts"] = events;

            return View();
        }

    }
}
