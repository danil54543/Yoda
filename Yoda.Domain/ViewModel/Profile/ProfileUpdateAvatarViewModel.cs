using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.ViewModel.Profile
{
    public class ProfileUpdateAvatarViewModel
    {
        public string Login { get; set; }
        public IFormFile img { get; set; }
    }
}
