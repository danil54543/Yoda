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
		
		public bool IsVerified { get; set; }


		[DataType(DataType.DateTime)]
		public DateTime TimeRegistration { get; set; }
		public long ProfileId { get; set; }
		public Profile Profile { get; set; }
        public IEnumerable<Todo>? TodoItems { get; set; }

    }
}
