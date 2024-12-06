namespace Web_prog_Project.Models.ViewModels
{
    public class ShiftWithDepartmentAndAssistant
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int AssistantId { get; set; }
        public Assistant Assistant { get; set; }
    }
}
