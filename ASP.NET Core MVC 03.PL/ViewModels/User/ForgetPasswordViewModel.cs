using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_MVC_03.PL.ViewModels.User
{
	public class ForgetPasswordViewModel
	{

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
