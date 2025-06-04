namespace Bookliz_Backend.Models
{
    public class UserLoginModel
    {
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty; // "Librarian", "Student", "Teacher"
    }
}