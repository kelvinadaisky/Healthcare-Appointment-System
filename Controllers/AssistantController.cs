using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_prog_Project.Data;
using Web_prog_Project.Models;

namespace Web_prog_Project.Controllers
{
    public class AssistantController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssistantController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            List<Assistant> objAssistantList = _db.Assistants.ToList();
            return View(objAssistantList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Assistant obj)
        {
            if (ModelState.IsValid)
            {
                // Extract the part before the "@" from the email
                string password = obj.Email.Substring(0, obj.Email.IndexOf('@'));

                // Create the ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = obj.FirstName, // Assuming Email is unique
                    Email = obj.Email,
                    Name = $"{obj.FirstName} {obj.LastName}" // Assuming you have a Name property
                };

                // Create the user with the generated password
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // Add assistant to the database
                    _db.Assistants.Add(obj);
                    await _db.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(user, "Patient");

                    TempData["success"] = "Assistant added successfully. Password is the part before @ in the email.";
                    return RedirectToAction("Index");
                }

            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Assistant? assistantFromDb = _db.Assistants.Find(id);
            if (assistantFromDb == null)
            {
                return NotFound();
            }
            return View(assistantFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Assistant obj)
        {
            if (ModelState.IsValid)
            {

                // Find the corresponding user in the UserManager by email
                var user = _userManager.FindByEmailAsync(obj.Email).Result;
                if (user != null)
                {
                    // Update user details (email, name, etc.)
                    user.Name = $"{obj.FirstName} {obj.LastName}";
                    user.UserName = $"{obj.FirstName}";
                    var updateResult = _userManager.UpdateAsync(user).Result;

                    if (updateResult.Succeeded)
                    {
                        // Update assistant details in the database
                        _db.Assistants.Update(obj);
                        _db.SaveChanges();

                        TempData["success"] = "Assistant updated successfully";
                        return RedirectToAction("Index");
                    }

                }


            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Assistant? assistantFromDb = _db.Assistants.Find(id);
            if (assistantFromDb == null)
            {
                return NotFound();
            }
            return View(assistantFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Assistant? obj = _db.Assistants.Find(id);
            if (obj == null)
            {
                return NotFound();
            }


            // Find the associated user
            var user = _userManager.FindByEmailAsync(obj.Email).Result;
            if (user != null)
            {
                // Remove the user from the role
                _userManager.RemoveFromRoleAsync(user, "Assistant").Wait();

                // Delete the user from UserManager
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    // Remove assistant from the database
                    _db.Assistants.Remove(obj);
                    _db.SaveChanges();

                    TempData["success"] = "Assistant and associated user deleted successfully";
                    return RedirectToAction("Index");
                }

            }

            TempData["error"] = "User not found.";
            return RedirectToAction("Index");

        }
    }
}