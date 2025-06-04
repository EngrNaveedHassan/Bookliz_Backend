using Bookliz_Backend.Data;
using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public class LibrarianService : ILibrarianService
    {
        private readonly AppDbContext _context;

        public LibrarianService(AppDbContext context)
        {
            _context = context;
        }


        public bool AddLibrarian(Librarian librarian)
        {
            try
            {
                _context.Librarians.Add(librarian);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding librarian: {ex.Message}");
                return false;
            }
        }

        public bool DeleteLibrarian(string empNumber)
        {
            try
            {
                var librarian = _context.Librarians.FirstOrDefault(l => l.EmpNumber == empNumber);
                if (librarian != null)
                {
                    _context.Librarians.Remove(librarian);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting librarian: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Librarian> GetAllLibrarians()
        {
            try
            {
                return _context.Librarians.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving librarians: {ex.Message}");
                return Enumerable.Empty<Librarian>();
            }
        }

        public Librarian GetLibrarianByEmpNumber(string empNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(empNumber))
                {
                    throw new ArgumentException("Employee number cannot be null or empty.", nameof(empNumber));
                }
                return _context.Librarians.FirstOrDefault(l => l.EmpNumber == empNumber)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving librarian: {ex.Message}");
                return null!;
            }
        }

        public bool UpdateLibrarian(Librarian librarian)
        {
            try
            {
                var existingLibrarian = _context.Librarians.FirstOrDefault(l => l.EmpNumber == librarian.EmpNumber);
                if (existingLibrarian != null)
                {
                    _context.Entry(existingLibrarian).CurrentValues.SetValues(librarian);
                    _context.Librarians.Update(existingLibrarian);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating librarian: {ex.Message}");
                return false;
            }
        }
    }
}