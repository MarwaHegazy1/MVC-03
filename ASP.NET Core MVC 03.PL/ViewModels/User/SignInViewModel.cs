using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_MVC_03.PL.ViewModels.User
{
    public class SignInViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
