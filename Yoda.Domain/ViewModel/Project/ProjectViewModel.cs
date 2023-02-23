using Yoda.Domain.Model;
using Yoda.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.ViewModel.Project
{
    public class ProjectViewModel
    {
        public long Id { get; set; }
        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Title { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        public ProjectCategory Category { get; set; }
        public string Login { get; set; }
    }
}
