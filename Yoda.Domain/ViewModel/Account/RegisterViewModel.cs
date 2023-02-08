using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.ViewModel.Account
{
	public class RegisterViewModel
	{

		[Required(ErrorMessage = "Enter your email.")]
		[MaxLength(50, ErrorMessage = "Email cannot contain more than 50 characters.")]
		[MinLength(3, ErrorMessage = "Email must be longer than 3 characters.")]
		[DataType(DataType.EmailAddress)]
		public string Login { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Enter a password.")]
		[MinLength(6, ErrorMessage = "Password cannot be less than 6 characters.")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Confirm the password.")]
		[Compare("Password", ErrorMessage = "Password mismatch!")]
		public string PasswordConfirm { get; set; }

		[Required(ErrorMessage = "Enter date of birth.")]
		[DataType(DataType.Date)]
		public DateTime BirdDate { get; set; }

		[Required(ErrorMessage = "Enter your name.")]
		[MinLength(2, ErrorMessage = "Name cannot be less than 2 characters.")]
		[MaxLength(50, ErrorMessage = "Name cannot contain more than 50 characters.")]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Enter your last name.")]
		[MinLength(2, ErrorMessage = "Last name cannot be less than 2 characters.")]
		[MaxLength(50, ErrorMessage = "Last name cannot contain more than 50 characters.")]
		[DataType(DataType.Text)]
		public string LastName { get; set; }
	}
}
