﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpYourChanel.Web.Controllers
{
    public class TagController : Controller
    {
        public IActionResult TagHelper()
        {
            return this.View();
        }
    }
}
