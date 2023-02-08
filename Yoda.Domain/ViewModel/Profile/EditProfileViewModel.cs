using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.ViewModel.Profile
{
	public class EditProfileViewModel
	{
		[Required, MinLength(2), MaxLength(50),DataType(DataType.Text)]
		public string FirstName { get; set; }


		[Required, MinLength(2), MaxLength(50),DataType(DataType.Text)]
		public string LastName { get; set; }


		[DataType(DataType.Date)]
		public DateTime BirdDate { get; set; }

		//TODO: Хранение изображения
		public byte[]? Image { get; set; }

	}
}
