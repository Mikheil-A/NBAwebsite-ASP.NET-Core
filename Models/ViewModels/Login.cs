using System.ComponentModel.DataAnnotations;

namespace NBAwebsite.Models.ViewModels
{
    public class Login
    {
        [Required, EmailAddress, Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
    }
}