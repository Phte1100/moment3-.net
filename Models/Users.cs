using System.ComponentModel.DataAnnotations;

namespace BookDirectory.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Låntagarens namn

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // Låntagarens e-post

        // En användare kan ha flera lån
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
