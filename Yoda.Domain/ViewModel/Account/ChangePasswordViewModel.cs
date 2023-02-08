using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.ViewModel.Account
{
	public class ChangePasswordViewModel
	{
		[Required(ErrorMessage = "Enter your email.")]
		public string UserLogin { get; set; } = null!;

		[Required(ErrorMessage = "Enter password")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[MinLength(6, ErrorMessage = "Password cannot be less than 6 characters.")]
		public string NewPassword { get; set; } = null!;
	}
}
