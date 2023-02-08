using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.ViewModel.Account
{
	public class RegisterViewModel
	{

		[Required(ErrorMessage = "Укажите Email")]
		[MaxLength(50, ErrorMessage = "Email должен иметь длину меньше 50 символов")]
		[MinLength(3, ErrorMessage = "Email должен иметь длину больше 3 символов")]
		public string Login { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Укажите пароль")]
		[MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Подтвердите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string PasswordConfirm { get; set; }

		[Required(ErrorMessage = "Введите дату рождения")]
		public DateTime BirdDate { get; set; }

		[Required(ErrorMessage = "Введиет имя")]
		[MinLength(2)]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Введиет фамилию")]
		[MinLength(2)]
		[MaxLength(50)]
		public string LastName { get; set; }
	}
}
