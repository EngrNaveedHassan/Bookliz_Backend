using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bookliz_Backend.Models
{
    public class Student : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? RegNumber { get; set; }
        public string? Department { get; set; }
    }
}