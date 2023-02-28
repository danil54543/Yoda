using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.Enum
{
    public enum ConpanyType
    {
        [Display(Name ="Company")]  Company = 0,
        [Display(Name ="Individual")]   Individual = 1,
    }
}
