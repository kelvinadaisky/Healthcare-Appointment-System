using Microsoft.AspNetCore.Mvc;
using Web_prog_Project.Data;
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

        public IActionResult Index()
        {
            List<FacultyMember> objFacultyList = _db.FacultyMembers.ToList();
            return View(objFacultyList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FacultyMember obj)
        {
            if (ModelState.IsValid)
            {
                _db.FacultyMembers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Faculty member added successfully";
                return RedirectToAction("Index");
            }
            return View();
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
