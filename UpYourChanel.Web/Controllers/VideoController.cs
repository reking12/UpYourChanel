using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChanel.Web.ViewModels;

namespace UpYourChanel.Web.Controllers
{
    public class VideoController : Controller
    {
        [HttpGet]
        public IActionResult AddVideo()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult AddVideo(AddVideoInputViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }
            return Json(input);
        }
    }
}
