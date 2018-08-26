using System.ComponentModel.DataAnnotations;

namespace NBAwebsite.Models.ViewModels
{
    public class Register
    {
        [Required, MaxLength(256), Display(Name = "Username")]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(50), DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, MinLength(4), MaxLength(50), DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password doesn't match the Confirmation Password.")]
        //[Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
