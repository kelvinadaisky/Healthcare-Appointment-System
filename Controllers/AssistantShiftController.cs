using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Data;
using Web_prog_Project.Models;
using Web_prog_Project.Utility;

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
            // Check if an assistant already has a shift on the same day in another department
            bool isDuplicateShift = await _context.AssistantShifts
                .AnyAsync(s => s.AssistantId == shift.AssistantId &&
                               s.ShiftDate.Date == shift.ShiftDate.Date &&
                               s.DepartmentId != shift.DepartmentId);

            if (isDuplicateShift)
            {
                TempData["error"] = "The assistant already has a shift in another department on this day.";
                ModelState.AddModelError(string.Empty, "The assistant already has a shift in another department on this day.");
            }
            if (ModelState.IsValid)
            {

                _context.AssistantShifts.Add(shift);
                await _context.SaveChangesAsync();
                TempData["success"] = "Shift added successfully";
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
                title = $"{s.Assistant.FirstName} {s.Assistant.LastName}<br>({s.StartTime.Hours:D2}:{s.StartTime.Minutes:D2} - {s.EndTime.Hours:D2}:{s.EndTime.Minutes:D2})", // Name and time
                start = new DateTime(s.ShiftDate.Year, s.ShiftDate.Month, s.ShiftDate.Day, s.StartTime.Hours, s.StartTime.Minutes, 0).ToString("yyyy-MM-ddTHH:mm:ss"),
                end = new DateTime(s.ShiftDate.Year, s.ShiftDate.Month, s.ShiftDate.Day, s.EndTime.Hours, s.EndTime.Minutes, 0).ToString("yyyy-MM-ddTHH:mm:ss"),
                extendedProps = new
                {
                    backgroundColor = Helper.GetDepartmentColor(s.DepartmentId), // Get color based on department ID
                    borderColor = "#162466", // Set a default border color
                    textColor = "white" // Set a default text color
                }
            }).ToList();
            // Pass the events to the view
            ViewData["Shifts"] = events;
            ViewData["Departments"] = await _context.Departments.ToListAsync(); // Fetch departments


            return View();
        }

    }
}
