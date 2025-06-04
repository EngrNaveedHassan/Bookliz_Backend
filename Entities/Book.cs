

namespace Bookliz_Backend.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Language { get; set; }
        public int Pages { get; set; }
        public string? CoverImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? BorrowerId { get; set; } // Assuming you have a User model and this is the ID of the user who borrowed the book
        public string? Location { get; set; } // e.g., "Shelf A1"
    }
}