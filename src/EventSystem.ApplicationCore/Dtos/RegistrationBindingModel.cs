using System.ComponentModel.DataAnnotations;

namespace EventSystem.ApplicationCore.Dtos
{
    public class RegistrationBindingModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
