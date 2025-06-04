using Bookliz_Backend.Data;
using Bookliz_Backend.Models;

namespace Bookliz_Backend.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void AddBooks(IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
            {
                throw new ArgumentNullException(nameof(books), "Books cannot be null or empty.");
            }

            _context.Books.AddRange(books);
            _context.SaveChanges();
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            if (books == null || !books.Any())
            {
                throw new KeyNotFoundException("No books found in the database.");
            }
            return books;
        }

        public IEnumerable<Book> GetAvailableBooks()
        {
            var availableBooks = _context.Books.Where(b => b.IsAvailable).ToList();
            if (availableBooks == null || !availableBooks.Any())
            {
                throw new KeyNotFoundException("No available books found in the database.");
            }
            return availableBooks;
        }

        public Book GetBookById(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }
            return book;
        }

        public Book GetBookByISBN(string isbn)
        {
            var book = _context.Books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ISBN {isbn} not found.");
            }
            return book;
        }

        public Book GetBookByTitle(string title)
        {
            var book = _context.Books.FirstOrDefault(b => b.Title!.ToLower() == title.ToLower());
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with title '{title}' not found.");
            }
            return book;
        }

        public IEnumerable<Book> GetBooksByBorrowerId(string borrowerId)
        {
            var books = _context.Books.Where(b => b.BorrowerId == borrowerId).ToList();
            if (books == null || !books.Any())
            {
                throw new KeyNotFoundException($"No books found for borrower ID {borrowerId}.");
            }
            return books;
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException("Search term cannot be null or empty.", nameof(searchTerm));
            }

            var books = _context.Books.Where(b => (b.Title != null && b.Title.Contains(searchTerm)) || (b.Author != null && b.Author.Contains(searchTerm))).ToList();
            if (books == null || !books.Any())
            {
                throw new KeyNotFoundException($"No books found matching the search term '{searchTerm}'.");
            }
            return books;
        }

        public void UpdateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            var existingBook = _context.Books.Find(book.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {book.Id} not found.");
            }

            _context.Entry(existingBook).CurrentValues.SetValues(book);
            _context.Books.Update(existingBook);
            _context.SaveChanges();
        }

        public void UpdateBookAvailability(int bookId, bool isAvailable)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            book.IsAvailable = isAvailable;
            _context.SaveChanges();
        }

        public void UpdateBookLocation(int bookId, string location)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            book.Location = location;
            _context.SaveChanges();
        }
    }
}