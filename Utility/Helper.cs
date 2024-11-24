using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_prog_Project.Utility
{
    public static class Helper
    {
        public const string Admin = "Admin";
        public const string Patient = "Patient";
        public const string Doctor = "Doctor";
        public const string appointmentAdded = "Appointment added successfully.";
        public const string appointmentUpdated = "Appointment updated successfully.";
        public const string appointmentDeleted = "Appointment deleted successfully.";
        public const string appointmentExists = "Appointment for selected date and time already exists.";
        public const string appointmentNotExists = "Appointment not exists.";
        public const string meetingConfirm = "Meeting confirm successfully.";
        public const string meetingConfirmError = "Error while confirming meeting.";
        public const string appointmentAddError = "Something went wront, Please try again.";
        public const string appointmentUpdatError = "Something went wront, Please try again.";
        public const string somethingWentWrong = "Something went wront, Please try again.";
        public const int success_code = 1;
        public const int failure_code = 0;

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=Helper.Admin,Text=Helper.Admin}
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=Helper.Patient,Text=Helper.Patient},
                    new SelectListItem{Value=Helper.Doctor,Text=Helper.Doctor}
                };
            }
        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute = minute + 30;
            }
            return duration;
        }
    }
}