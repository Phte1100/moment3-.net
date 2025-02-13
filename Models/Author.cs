using System.ComponentModel.DataAnnotations;

namespace BookDirectory.Models
{
    public class Author
    { 
        public int Id { get; set; }

        public required string AuthorName { get; set; } 

        public required int Born { get; set; }

        // En författare kan ha flera böcker
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
