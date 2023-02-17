using System.ComponentModel.DataAnnotations;
using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
	public class Todo
	{
		public long Id { get; set; }


		[Required,MaxLength(50),MinLength(1),DataType(DataType.Text)]
		public string Title { get; set; }


		[Required,MinLength(1),DataType(DataType.MultilineText)]
		public string Item { get; set; }


		[DataType(DataType.DateTime)]
		public DateTime DateCreate { get; set; }


		public Marker? Marker { get; set; }
		public long UserId { get; set; }
		public User User { get; set; }
	}
}
