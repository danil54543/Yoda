using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.ViewModel.Account
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Enter your email.")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Enter your password.")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
	}
}
