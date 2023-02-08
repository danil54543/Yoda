using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
	public class User
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
	}
}
	