using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Data;
using Web_prog_Project.Models.ViewModels;
using Web_prog_Project.Models;


namespace Web_prog_Project.Controllers
{
    public class FacultyMemberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FacultyMemberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
                var facultyMembers = await _db.FacultyMembers
                 .Join(
                     _db.Departments,
                     fm => fm.DepartmentId,
                     d => d.DepartmentId,
                     (fm, d) => new FacultyMemberWithDepartment
                     {
                         FacultyMemberId = fm.FacultyMemberId,
                         FirstName = fm.FirstName,
                         LastName = fm.LastName,
                         Phone = fm.Phone,
                         Email = fm.Email,
                         DepartmentName = d.Name
                     }
                 )
                 .ToListAsync();

            return View(facultyMembers);
        }

        public IActionResult Create()
        {
            // Populate Departments for the dropdown
            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacultyMember obj)
        {
            if (ModelState.IsValid)
            {
                _db.FacultyMembers.Add(obj);
                await _db.SaveChangesAsync();
                TempData["success"] = "Faculty member updated successfully";
                return RedirectToAction("Index");
            }

            // Repopulate Departments if model validation fails
            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            FacultyMember? facultyFromDb = _db.FacultyMembers.Find(id);
            if (facultyFromDb == null)
            {
                return NotFound();
            }
            // Populate Departments for the dropdown
            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View(facultyFromDb);
        }

        [HttpPost]
        public IActionResult Edit(FacultyMember obj)
        {
            if (ModelState.IsValid)
            {
                _db.FacultyMembers.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Faculty member updated successfully";
                return RedirectToAction("Index");
            }

            // Populate Departments for the dropdown
            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            FacultyMember? facultyFromDb = _db.FacultyMembers.Find(id);
            if (facultyFromDb == null)
            {
                return NotFound();
            }
            return View(facultyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            FacultyMember? obj = _db.FacultyMembers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.FacultyMembers.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Faculty member deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
