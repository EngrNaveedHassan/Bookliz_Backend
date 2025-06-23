using Bookliz_Backend.Data;
using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStudentAsync(Student student)
        {
            if (student == null)
                return false;

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(string RegNumber)
        {
            var student = await _context.Students.FindAsync(RegNumber);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            var result = _context.Students.ToList();
            result.Where(s => s.marks > 50);
            return result;
        }

        public async Task<Student> GetStudentByRegNumberAsync(string RegNumber)
        {
            var student = await _context.Students.FindAsync(RegNumber);
            if (student == null)
                throw new KeyNotFoundException($"Student with RegNumber {RegNumber} not found.");

            return student;
        }

        public async Task<bool> UpdateStudentAsync(string RegNumber, Student student)
        {
            if (student == null)
                return false;

            var existingStudent = await _context.Students.FindAsync(RegNumber);
            if (existingStudent == null)
                return false;

            _context.Entry(existingStudent).CurrentValues.SetValues(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}