using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChanel.Web.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            Videos = new HashSet<Video>();
        }

        public IEnumerable<Video> Videos { get; set; }
    }
}
