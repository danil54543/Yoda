using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.ViewModel.Project
{
    public class ProjectInfoViewModel
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
