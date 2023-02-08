namespace Yoda.Domain.Model
{
	public class Profile
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime BirdDate { get; set; }
		public byte[]? Image { get; set; }
		public IEnumerable<TodoItem>? TodoItems { get; set; }
	}
}
