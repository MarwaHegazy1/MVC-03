using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_MVC_03.PL.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password is required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match with Password")]
        public string ConfirmPassword { get; set; }
    }
}
