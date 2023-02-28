using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.Enum
{
    public enum ProjectCategory
    {
        [Display (Name ="Shop")]    Shop = 0,
        [Display(Name = "BaberShop")]   BaberShop = 1,
        [Display(Name = "Parking")]    Parking = 2
    }
}
