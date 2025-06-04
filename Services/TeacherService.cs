using Bookliz_Backend.Data;
using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public class TeacherService : ITeacherService
    {

        private readonly AppDbContext _context;

        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTeacherAsync(Teacher teacher)
        {
            if (teacher == null)
                return false;

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeacherAsync(string EmpNumber)
        {
            var teacher = await _context.Teachers.FindAsync(EmpNumber);
            if (teacher == null)
                return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<Teacher> GetAllTeachers()
        {
            return _context.Teachers.ToList();
        }

        public async Task<Teacher> GetTeacherByEmpNumberAsync(string EmpNumber)
        {
            var teacher = await _context.Teachers.FindAsync(EmpNumber);
            if (teacher == null)
                throw new KeyNotFoundException($"Teacher with EmpNumber {EmpNumber} not found.");

            return teacher;
        }

        public async Task<bool> UpdateTeacherAsync(string EmpNumber, Teacher teacher)
        {
            if (teacher == null)
                return false;

            var existingTeacher = await _context.Teachers.FindAsync(EmpNumber);
            if (existingTeacher == null)
            {
                return false;
            }
            _context.Entry(existingTeacher).CurrentValues.SetValues(teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}