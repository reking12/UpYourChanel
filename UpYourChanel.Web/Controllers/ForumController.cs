using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChannel.Web.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Forum()
        {
            return this.View();
        }
    }
}
