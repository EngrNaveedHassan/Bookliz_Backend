using Bookliz_Backend.Models;
using Bookliz_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookliz_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/book/getbooks
        [HttpGet("getbooks")]
        public IActionResult GetBooks()
        {
            return Ok(_bookService.GetAllBooks());
        }

        // GET: api/book/getbookbyid
        [HttpGet("getbookbyid")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        // GET: api/book/getbookbytitle
        [HttpGet("getbookbytitle")]
        public IActionResult GetBook(string title)
        {
            var book = _bookService.GetBookByTitle(title);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        // GET: api/book/getbookbyISBN
        [HttpGet("getbookbyISBN")]
        public IActionResult GetBookByISBN(string isbn)
        {
            var book = _bookService.GetBookByISBN(isbn);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        // GET: api/book/searchbooks
        [HttpGet("searchbooks")]
        public IActionResult SearchBooks(string searchString)
        {
            var books = _bookService.SearchBooks(searchString);
            if (books == null || !books.Any())
            {
                return NotFound("No books found.");
            }
            return Ok(books);
        }

        // GET: api/book/getbooksbybrowserid/{id}
        [HttpGet("getbooksbybrowserid")]
        public IActionResult GetBooksByBrowserId(string id)
        {
            var books = _bookService.GetBooksByBorrowerId(id);
            if (books == null || !books.Any())
            {
                return NotFound("No books found for this browser ID.");
            }
            return Ok(books);
        }

        // GET: api/book/getavailablebooks
        [HttpGet("getavailablebooks")]
        public IActionResult GetAvailableBooks()
        {
            var books = _bookService.GetAvailableBooks();
            if (books == null || !books.Any())
            {
                return NotFound("No available books found.");
            }
            return Ok(books);
        }


        // POST: api/book/addbook
        [Authorize(Roles = "Librarian")]
        [HttpPost("addbook")]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }

            _bookService.AddBook(book);
            return Ok("Book Added Successfully.");
        }

        // POST: api/book/addbooks
        [Authorize(Roles = "Librarian")]
        [HttpPost("addbooks")]
        public IActionResult AddBooks([FromBody] IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
            {
                return BadRequest("Books cannot be null or empty.");
            }

            _bookService.AddBooks(books);
            return Ok("Books Added Successfully.");
        }

        // PUT: api/book/updatebook
        [Authorize(Roles = "Librarian")]
        [HttpPut("updatebook")]
        public IActionResult UpdateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }

            _bookService.UpdateBook(book);
            return Ok("Book Updated Successfully.");
        }

        // PUT: api/book/updatebooklocation
        [Authorize(Roles = "Librarian")]
        [HttpPut("updatebooklocation")]
        public IActionResult UpdateBookLocation(int id, string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("Location cannot be null or empty.");
            }

            _bookService.UpdateBookLocation(id, location);
            return Ok("Book Location Updated Successfully.");
        }

        // PUT: api/book/updatebookavailability
        [HttpPut("updatebookavailability")]
        public IActionResult UpdateBookAvailability(int id, bool isAvailable)
        {
            _bookService.UpdateBookAvailability(id, isAvailable);
            return Ok("Book Availability Updated Successfully.");
        }

        // DELETE: api/book/deletebook
        [Authorize(Roles = "Librarian")]
        [HttpDelete("deletebook")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return Ok("Book Deleted Successfully.");
        }
    }
}