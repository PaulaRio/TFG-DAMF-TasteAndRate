using TasteAndRateAPI.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TasteAndRateAPI.Models.DTOs.UserDto
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Field required: Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Field required: Email")]
        [EmailValidation]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field required: Password")]
        [PasswordValidation]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field required: Role")]
        public string Role { get; set; }

    }
}
