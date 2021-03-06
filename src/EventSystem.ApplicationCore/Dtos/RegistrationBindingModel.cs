using System.ComponentModel.DataAnnotations;

namespace EventSystem.ApplicationCore.Dtos
{
    public class RegistrationBindingModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
