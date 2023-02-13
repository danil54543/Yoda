using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.Model
{
    public class Profile
    {
        public long Id { get; set; }
        [Required, MinLength(2), MaxLength(50), DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50), DataType(DataType.Text)]
        public string LastName { get; set; }


        public byte Age { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirdDate { get; set; }

        //TODO: Хранение изображения
        public byte[]? Image { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

    }
}
