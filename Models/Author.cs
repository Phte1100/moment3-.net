using System.ComponentModel.DataAnnotations;

namespace AuthorDirectory.Models
{
    public class Author
    { 
        // Properties
        public int Id { get; set; }
        public required string AuthorName { get; set; } 
        public int Born { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}