using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.ViewModel.User
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Specify role.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Enter email."), DataType(DataType.EmailAddress)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TimeRegistration { get; set; }
    }
}
