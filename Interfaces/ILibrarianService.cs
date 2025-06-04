using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public interface ILibrarianService
    {
        bool AddLibrarian(Librarian librarian);
        bool UpdateLibrarian(Librarian librarian);
        bool DeleteLibrarian(string empNumber);
        Librarian GetLibrarianByEmpNumber(string empNumber);
        IEnumerable<Librarian> GetAllLibrarians();
    }
}