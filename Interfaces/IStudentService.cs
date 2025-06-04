using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentByRegNumberAsync(string RegNumber);
        IEnumerable<Student> GetAllStudents();
        Task<bool> CreateStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(string RegNumber, Student student);
        Task<bool> DeleteStudentAsync(string RegNumber);
    }
}