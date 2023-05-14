using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ContactApp.Attribute;


// User or a Contact model
namespace ContactApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        [UniqueEmail(ErrorMessage = "Email address already exists.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; } = DateTime.Now;
    }
}
