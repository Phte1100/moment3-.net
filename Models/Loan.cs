using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookDirectory.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; } // Främmande nyckel till Book

        [Required]
        public int UserId { get; set; } // Främmande nyckel till User

        [Required]
        public DateTime LoanDate { get; set; } = DateTime.Now; // Datum för utlåning

        public DateTime? ReturnDate { get; set; } // Datum för återlämning, nullable

        // Navigeringsproperties
        public Book? Book { get; set; }
        public User? User { get; set; }
    }
}
