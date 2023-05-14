using ContactApp.Db;
using System.ComponentModel.DataAnnotations;

namespace ContactApp.Attribute
{
    // Attribute which should handle checking if an email is unique
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dbContext = (ContactAppDbContext)validationContext.GetService(typeof(ContactAppDbContext));
            var email = value.ToString();
            
            if(dbContext.Users.Any(u => u.Email == email))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
