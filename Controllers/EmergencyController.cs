﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Web_prog_Project.Data;
using Web_prog_Project.Models;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace Web_prog_Project.Controllers
{
    public class EmergencyController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public EmergencyController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var announcements = _db.EmergencyAnnouncements.OrderByDescending(e => e.CreatedAt).ToList();
            return View(announcements);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmergencyAnnouncement announcement)
        {
            if (ModelState.IsValid)
            {
                _db.EmergencyAnnouncements.Add(announcement);
                _db.SaveChanges();

                await SendEmailToAllUsersAsync(announcement);

                TempData["success"] = "Emergency announcement added successfully.";
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        private async Task SendEmailToAllUsersAsync(EmergencyAnnouncement announcement)
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var subject = "Emergency Announcement: " + announcement.Title;
                var body = $"<h1>{announcement.Title}</h1><p>{announcement.Content}</p>";

                try
                {
                    await _emailSender.SendEmailAsync(user.Email, subject, body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email to {user.Email}: {ex.Message}");
                }
            }
            TempData["emailSuccess"] = "Emergency announcement emails sent to all users.";

        }

        public IActionResult Delete(int id)
        {
            var announcement = _db.EmergencyAnnouncements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var announcement = _db.EmergencyAnnouncements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }

            _db.EmergencyAnnouncements.Remove(announcement);
            _db.SaveChanges();

            TempData["success"] = "Emergency announcement deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
