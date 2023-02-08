using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
	public class TodoItem
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Item { get; set; }
		public DateTime DateCreate { get; set; }
		public Priority? Priority { get; set; }
		public Marker? Marker { get; set; }
		public long ProfileId { get; set; }
		public Profile Profile { get; set; }
	}
}
