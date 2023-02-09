using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoda.Domain.Enum;

namespace Yoda.Domain.ViewModel.Todo
{
	public class TodoViewModel
	{
		public long Id { get; set; }

		[Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
		public string Title { get; set; }

		[Required, DataType(DataType.DateTime)]
		public DateTime DateCreate { get; set; }

		public Priority? Priority { get; set; }
		public Marker? Marker { get; set; }
	}
}
