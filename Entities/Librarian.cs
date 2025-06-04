using System.ComponentModel.DataAnnotations;


namespace Bookliz_Backend.Models
{
    public class Librarian : User
    {
        [Key]
        public string? EmpNumber { get; set; }
        public string? Designation { get; set; }
    }
}