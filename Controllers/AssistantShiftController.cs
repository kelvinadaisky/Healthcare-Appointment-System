using Microsoft.AspNetCore.Identity;
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
        private readonly ApplicationDbContext _db;

        public AssistantShiftController(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult Create()
        {
            ViewData["Assistants"] = _db.Assistants.Select(a => new SelectListItem
            {
                Value = a.AssistantId.ToString(),
                Text = a.FirstName,
            }).ToList();

            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
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
            bool isDuplicateShift = await _db.AssistantShifts
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

                _db.AssistantShifts.Add(shift);
                await _db.SaveChangesAsync();
                TempData["success"] = "Shift added successfully";
                return RedirectToAction("Schedule");
            }
            ViewData["Assistants"] = _db.Assistants.Select(a => new SelectListItem
            {
                Value = a.AssistantId.ToString(),
                Text = a.FirstName,
            }).ToList();

            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();
            return View(shift);
        }

        public async Task<IActionResult> Schedule()
        {
            // Fetch the assistant shifts, including the assistant's name and shift date
            var shifts = await _db.AssistantShifts
                .Include(s => s.Assistant) 
                .ToListAsync();

            // Prepare the events for the calendar
            var events = shifts.Select(s => new
            {
                title = $"{s.Assistant.FirstName} {s.Assistant.LastName}<br>({s.StartTime.Hours:D2}:{s.StartTime.Minutes:D2} - {s.EndTime.Hours:D2}:{s.EndTime.Minutes:D2})", // Name and time
                start = new DateTime(s.ShiftDate.Year, s.ShiftDate.Month, s.ShiftDate.Day, s.StartTime.Hours, s.StartTime.Minutes, 0).ToString("yyyy-MM-ddTHH:mm:ss"),
                end = new DateTime(s.ShiftDate.Year, s.ShiftDate.Month, s.ShiftDate.Day, s.EndTime.Hours, s.EndTime.Minutes, 0).ToString("yyyy-MM-ddTHH:mm:ss"),
                extendedProps = new
                {
                    backgroundColor = Helper.GetDepartmentColor(s.DepartmentId), 
                    borderColor = "#162466", 
                    textColor = "white" 
                }
            }).ToList();
            // Pass the events to the view
            ViewData["Shifts"] = events;
            ViewData["Departments"] = await _db.Departments.ToListAsync(); // Fetch departments


            return View();
        }

        public IActionResult List()
        {
            var shifts = _db.AssistantShifts.Include(s => s.Department).Include(s => s.Assistant).ToList();
            return View(shifts);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            AssistantShift? assistantShiftFromDb = _db.AssistantShifts
                .Include(s => s.Assistant) 
                .FirstOrDefault(s => s.Id == id);

            if (assistantShiftFromDb == null)
            {
                return NotFound();
            }

            ViewData["Assistants"] = _db.Assistants.Select(a => new SelectListItem
            {
                Value = a.AssistantId.ToString(),
                Text = a.FirstName,
            }).ToList();

            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View(assistantShiftFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AssistantShift obj)
        {
            bool isDuplicateShift = await _db.AssistantShifts
                .AnyAsync(s => s.AssistantId == obj.AssistantId &&
                               s.ShiftDate.Date == obj.ShiftDate.Date &&
                               s.DepartmentId != obj.DepartmentId);

            if (isDuplicateShift)
            {
                TempData["error"] = "The assistant already has a shift in another department on this day.";
                ModelState.AddModelError(string.Empty, "The assistant already has a shift in another department on this day.");
            }

            if (ModelState.IsValid)
            {
                _db.AssistantShifts.Update(obj);
                await _db.SaveChangesAsync();

                TempData["success"] = "Shift updated successfully";
                return RedirectToAction("Schedule");
            }

            // Repopulate ViewData if the model state is invalid
            ViewData["Assistants"] = await _db.Assistants.Select(a => new SelectListItem
            {
                Value = a.AssistantId.ToString(),
                Text = a.FirstName,
            }).ToListAsync();

            ViewData["Departments"] = await _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToListAsync();

            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            AssistantShift? assistantShiftFromDb = _db.AssistantShifts
                .Include(s => s.Assistant)
                .Include(s => s.Department)
                .FirstOrDefault(s => s.Id == id);

            if (assistantShiftFromDb == null)
            {
                return NotFound();
            }
            return View(assistantShiftFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            AssistantShift? obj = _db.AssistantShifts.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.AssistantShifts.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Assistant and associated user deleted successfully";
            return RedirectToAction("Schedule");


        }
    }
}
