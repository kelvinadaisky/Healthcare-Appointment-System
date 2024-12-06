using System.ComponentModel.DataAnnotations;

namespace Web_prog_Project.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available beds must be a positive number.")]
        public int AvailableBeds { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of patients must be a positive number.")]
        public int NumberOfPatients { get; set; }

        public ICollection<AssistantShift> AssistantShifts { get; set; } = new List<AssistantShift>();

    }

}
