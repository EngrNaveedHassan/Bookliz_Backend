using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public interface IBookService
    {
        void AddBook(Book book);
        void AddBooks(IEnumerable<Book> books);
        void UpdateBook(Book book);
        void UpdateBookLocation(int bookId, string location);
        void UpdateBookAvailability(int bookId, bool isAvailable);
        void DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int bookId);
        Book GetBookByISBN(string isbn);
        Book GetBookByTitle(string title);
        IEnumerable<Book> SearchBooks(string searchTerm);
        IEnumerable<Book> GetBooksByBorrowerId(string borrowerId);
        IEnumerable<Book> GetAvailableBooks();
    }
}