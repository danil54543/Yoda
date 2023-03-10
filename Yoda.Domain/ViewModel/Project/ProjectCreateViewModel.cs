using Yoda.Domain.Model;
using Yoda.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Yoda.Domain.ViewModel.Project
{
    public class ProjectCreateViewModel
    {
        public long Id { get; set; }
        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Title { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        [Required]
        public string ConpanyType { get; set; }

        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Country { get; set; }


        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string City { get; set; }

        }
}
