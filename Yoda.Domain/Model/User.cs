using System.ComponentModel.DataAnnotations;
using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
	public class User
	{
		public long Id { get; set; }


		[Required, MaxLength(50), MinLength(3)]
		public string Email { get; set; }

		[Required, MinLength(6)]
		public string Password { get; set; }


		public Role Role { get; set; }
		[Required, MinLength(2), MaxLength(50)]
		public string FirstName { get; set; }

		[Required, MinLength(2), MaxLength(50)]
		public string LastName { get; set; }


		public byte Age { get; set; }
		public DateTime BirdDate { get; set; }
		public byte[]? Image { get; set; }
		public bool IsVerified { get; set; }
		public DateTime TimeRegistration { get; set; }
		public IEnumerable<Todo>? TodoItems { get; set; }
	}
}
