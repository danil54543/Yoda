using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Yoda.Domain.ViewModel.Account
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Введите Email")]
		[MaxLength(20, ErrorMessage = "Email должен иметь длину меньше 20 символов")]
		[MinLength(3, ErrorMessage = "Email должен иметь длину больше 3 символов")]
		public string Login { get; set; } = null!;

		[Required(ErrorMessage = "Введите пароль")]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; } = null!;
	}
}
