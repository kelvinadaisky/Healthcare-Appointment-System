namespace Web_prog_Project.Models
{
    public class AdminSchedule
    {
        public int AdminScheduleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DoctorId { get; set; }
        public string AdminId { get; set; }

    }
}
