using System.ComponentModel.DataAnnotations;

namespace Challenge.Backend.Model
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "CUIT is required.")]
        [RegularExpression(@"^\d{2}-\d{8}-\d$", ErrorMessage = "CUIT must be in the format XX-XXXXXXXX-X.")]
        public string? CUIT { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Cellphone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? Cellphone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
    }
}
