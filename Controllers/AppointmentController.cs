using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_prog_Project.Services;
using Web_prog_Project.Utility;

namespace Web_prog_Project.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AppointmentController(IAppointmentService appointmentService , IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;

        }

        public IActionResult Index()
        {

            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.DoctorList = _appointmentService.GetDoctorList();
            if (User.IsInRole(Web_prog_Project.Utility.Helper.Patient))
            {
                // Get the logged-in patient's ID
                var loggedInPatientId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Fetch the logged-in patient's name
                ViewBag.PatientInfo = _appointmentService.GetPatientInfo(loggedInPatientId);

            }
            else
            {
                ViewBag.PatientList = _appointmentService.GetPatientList();
            }
            return View();
        }
    }
}