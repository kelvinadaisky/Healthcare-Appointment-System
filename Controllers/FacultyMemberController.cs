﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Data;
using Web_prog_Project.Models.ViewModels;
using Web_prog_Project.Models;
using Microsoft.AspNetCore.Identity;


namespace Web_prog_Project.Controllers
{
    public class FacultyMemberController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public FacultyMemberController(ApplicationDbContext db,UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
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
            if (string.IsNullOrWhiteSpace(obj.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (ModelState.IsValid)
            {
                string password = obj.Email.Substring(0, obj.Email.IndexOf('@'));
                var user = new ApplicationUser
                {
                    UserName = obj.FirstName, 
                    Email = obj.Email,
                    Name = $"{obj.FirstName} {obj.LastName}" 
                };
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    _db.FacultyMembers.Add(obj);
                    await _db.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(user, "Doctor");
                    TempData["success"] = "Faculty member created successfully";
                    return RedirectToAction("Index");
                }
                   
            }

            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            var facultyMember = _db.FacultyMembers
                .FirstOrDefault(f => f.FacultyMemberId == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var departments = _db.Departments
                .Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                }).ToList();



            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View(facultyMember);
        }

        [HttpPost]
        public IActionResult Edit(FacultyMember obj)
        {

            if (ModelState.IsValid)
            {
                var facultyMember = _db.FacultyMembers
           .FirstOrDefault(f => f.FacultyMemberId == obj.FacultyMemberId); 

                if (facultyMember != null)
                {
                    facultyMember.FirstName = obj.FirstName;
                    facultyMember.LastName = obj.LastName;
                    facultyMember.Phone = obj.Phone;
                    facultyMember.Email = obj.Email;
                    facultyMember.DepartmentId = obj.DepartmentId; 

                    var user = _userManager.FindByEmailAsync(obj.Email).Result;
                    if (user != null)
                    {
                        user.Name = $"{obj.FirstName} {obj.LastName}";
                        user.UserName = $"{obj.FirstName}";
                        var updateResult = _userManager.UpdateAsync(user).Result;

                        if (updateResult.Succeeded)
                        {
                            _db.SaveChanges();
                            TempData["success"] = "Faculty Member updated successfully";
                            return RedirectToAction("Index");
                        }
                    }
                            
                }

            }

            ViewData["Departments"] = _db.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            }).ToList();

            return View();
        }

        public IActionResult Delete(int? id)
        {
            var facultyMember = _db.FacultyMembers
                    .FirstOrDefault(f => f.FacultyMemberId == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var departmentName = _db.Departments
                .Where(d => d.DepartmentId == facultyMember.DepartmentId)
                .Select(d => d.Name) 
                .FirstOrDefault();

            // Create the view model to pass to the view
            var viewModel = new FacultyMemberWithDepartment
            {
                FacultyMemberId = facultyMember.FacultyMemberId,
                FirstName = facultyMember.FirstName,
                LastName = facultyMember.LastName,
                DepartmentName = departmentName, 
                Phone = facultyMember.Phone,
                Email = facultyMember.Email
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            FacultyMember? obj = _db.FacultyMembers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
          
            var user = _userManager.FindByEmailAsync(obj.Email).Result;
            if (user != null)
            {
                var appointments = _db.Appointments.Where(a => a.DoctorId == user.Id).ToList();
                if (appointments.Any())
                {
                    _db.Appointments.RemoveRange(appointments);
                }

                _db.FacultyMembers.Remove(obj);
                _db.SaveChanges();

                _userManager.RemoveFromRoleAsync(user, "Faculty Member").Wait();

                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {              
                    TempData["success"] = "Faculty Member and associated user deleted successfully";
                    return RedirectToAction("Index");
                }


            }

            TempData["error"] = "User not found.";
            return RedirectToAction("Index");

        }
    }
}