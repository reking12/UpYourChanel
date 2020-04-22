using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels.Message;

namespace UpYourChannel.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public LoginModel(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public int Count => OnGetAsync().GetAwaiter().GetResult();
        public async Task<int> OnGetAsync()
        {
            var user = await _userManager.Users.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));
            return user.Messages.Count();
        }
    }
}
