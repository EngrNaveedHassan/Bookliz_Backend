using System.ComponentModel.DataAnnotations;


namespace Bookliz_Backend.Models
{
    public class Teacher : User
    {
        [Key]
        public string? EmpNumber { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }

    }
}