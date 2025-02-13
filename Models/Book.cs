using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookDirectory.Models
{
    public class Book
    { 
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Isbn { get; set; } 

        public required int Year { get; set; }

        // Främmande nyckel för Author
        public int AuthorId { get; set; }

        // Navigeringsproperty till Author
        public Author? Author { get; set; }

        // Är boken tillgänglig för lån?
        public bool IsAvailable { get; set; } = true;

        // En bok kan ha flera lån
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
