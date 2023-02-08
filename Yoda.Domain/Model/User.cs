using System.ComponentModel.DataAnnotations;
using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
	public class User
	{
		public long Id { get; set; }


		[Required, MaxLength(50), MinLength(3),DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required, MinLength(6),DataType(DataType.Password)]
		public string Password { get; set; }


		public Role Role { get; set; }
		[Required, MinLength(2), MaxLength(50), DataType(DataType.Text)]
		public string FirstName { get; set; }

		[Required, MinLength(2), MaxLength(50),DataType(DataType.Text)]
		public string LastName { get; set; }


		public byte Age { get; set; }
		[DataType(DataType.Date)]
		public DateTime BirdDate { get; set; }

		//TODO: Хранение изображения
		public byte[]? Image { get; set; }
		public bool IsVerified { get; set; }


		[DataType(DataType.DateTime)]
		public DateTime TimeRegistration { get; set; }

		public IEnumerable<Todo>? TodoItems { get; set; }
	}
}
