using Microsoft.AspNetCore.Identity.UI.Services;
using Web_prog_Project.Data;
using Web_prog_Project.Models;
using Web_prog_Project.Utility;

namespace Web_prog_Project.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AppointmentVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
            var patient = _db.Users.FirstOrDefault(u => u.Id == model.PatientId);
            var doctor = _db.Users.FirstOrDefault(u => u.Id == model.DoctorId);
            if (model != null && model.Id > 0)
            {
                //update
                var appointment = _db.Appointments.FirstOrDefault(x => x.Id == model.Id);
                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.StartDate = startDate;
                appointment.EndDate = endDate;
                appointment.Duration = model.Duration;
                appointment.DoctorId = model.DoctorId;
                appointment.PatientId = model.PatientId;
                appointment.IsDoctorApproved = false;
                appointment.AdminId = model.AdminId;
                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                //create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId
                };

                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }

        }

        public async Task<int> ConfirmEvent(int id, string patientId)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                appointment.IsDoctorApproved = true;
                appointment.PatientId = patientId; // Update the PatientId if necessary
                return await _db.SaveChangesAsync();
            }
            return 0;
        }


        public async Task<int> Delete(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                _db.Appointments.Remove(appointment);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteBookedAppointment(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null)
            {
                appointment.IsDoctorApproved = false;
                appointment.PatientId = null; // Update the PatientId if necessary
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public List<AppointmentVM> DoctorsEventsById(string doctorId)
        {
            return _db.Appointments.Where(x => x.DoctorId == doctorId).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }

        public AppointmentVM GetById(int id)
        {
            return _db.Appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _db.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault(),
            }).SingleOrDefault();
        }

        public List<DoctorVM> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
        }

        public List<AssistantVM> GetPatientList()
        {
            var patients = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.Assistant) on userRoles.RoleId equals roles.Id
                            select new AssistantVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                           ).ToList();

            return patients;
        }

        public List<AppointmentVM> PatientsEventsById(string patientId)
        {
            return _db.Appointments.Where(x => x.PatientId == patientId || x.PatientId == null).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId = c.PatientId
            }).ToList();
        }

        public List<AssistantVM> GetPatientInfo(string patientId)
        {
            // Vérifiez si le patientId est null ou vide
            if (string.IsNullOrEmpty(patientId))
            {
                return new List<AssistantVM>(); // Retourne une liste vide si l'ID n'est pas valide
            }

            var patients = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.Assistant) on userRoles.RoleId equals roles.Id
                            where user.Id == patientId // Filtrer par patientId
                            select new AssistantVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                           ).ToList();

            return patients;
        }
    }
}