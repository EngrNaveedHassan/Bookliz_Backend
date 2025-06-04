using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        Task<Teacher> GetTeacherByEmpNumberAsync(string EmpNumber);
        Task<bool> CreateTeacherAsync(Teacher teacher);
        Task<bool> UpdateTeacherAsync(string EmpNumber, Teacher teacher);
        Task<bool> DeleteTeacherAsync(string EmpNumber);
    }
}