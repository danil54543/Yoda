using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
	public class Todo
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Item { get; set; }
		public DateTime DateCreate { get; set; }
		public Priority? Priority { get; set; }
		public Marker? Marker { get; set; }
		public long UserId { get; set; }
		public User User { get; set; }
	}
}
