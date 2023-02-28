using Yoda.Domain.Model;
using Yoda.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Yoda.Domain.ViewModel.Project
{
    public class ProjectViewModel
    {
        public long Id { get; set; }
        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Title { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        [Required]
        public string Category { get; set; }

        public IFormFile Img { get; set; }

        public byte[]? Logo;


        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Country { get; set; }


        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string City { get; set; }


        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Street { get; set; }

        public int Build { get; set; }


        [Required, MaxLength(15), DataType(DataType.Text)]
        public string PhoneNum { get; set; }


        [MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Login { get; set; }
    }
}
