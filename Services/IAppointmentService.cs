using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_prog_Project.Models;

namespace Web_prog_Project.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorList();
        public List<AssistantVM> GetPatientList();
        public List<AssistantVM> GetPatientInfo(string patientId);
        public Task<int> AddUpdate(AppointmentVM model);

        public List<AppointmentVM> DoctorsEventsById(string doctorId);

        public List<AppointmentVM> PatientsEventsById(string patientId);

        public AppointmentVM GetById(int id);

        public Task<int> Delete(int id);

        public Task<int> DeleteBookedAppointment(int id); // Add this line if not already present


        public Task<int> ConfirmEvent(int id , string patientId);


    }
}