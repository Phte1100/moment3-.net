using System.ComponentModel.DataAnnotations;

namespace BookDirectory.Models
{
    public class Book
    { 
        // Properties
        public int Id { get; set; }
        public required string Title { get; set; } 
        public required string Author { get; set; }
        public int Year { get; set; }

        public int AuthorId { get; set; } // Främmande nyckel
        public Author Author { get; set; } // Navigeringsproperty
    }
}