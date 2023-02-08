using System.ComponentModel.DataAnnotations;
using Yoda.Domain.Enum;

namespace Yoda.Domain.ViewModel.Todo
{
	public class CreateTodoViewModel
	{
		public int Id { get; set; }

		[Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
		public string Title { get; set; }


		[Required, MinLength(1), DataType(DataType.MultilineText)]
		public string Item { get; set; }


		[Required,DataType(DataType.DateTime)]
		public DateTime DateCreate { get; set; }


		public Priority? Priority { get; set; }
		public Marker? Marker { get; set; }


		[DataType(DataType.EmailAddress)]
		public string Login { get; set; }
	}
}
