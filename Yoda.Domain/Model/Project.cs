using System.ComponentModel.DataAnnotations;
using Yoda.Domain.Enum;

namespace Yoda.Domain.Model
{
    public class Project
    {
        public long Id { get; set; }

        [Required, MaxLength(50), MinLength(1), DataType(DataType.Text)]
        public string Title { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        public ProjectCategory Category { get; set; }
        public byte[]? Image { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
