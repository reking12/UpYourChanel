﻿using System;
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
using UpYourChannel.Web.Paging;
using UpYourChannel.Web.Services;
using UpYourChannel.Web.ViewModels.Message;

namespace UpYourChannel.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper mapper;
        private readonly IMessageService messageService;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.mapper = mapper;
            this.messageService = messageService;
        }

        public IEnumerable<MessageViewModel> Messages { get; set; }

        public PaginatedList<MessageViewModel> PaginatedMessages { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user, int? pageNumber)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            ProfilePictureUrl = user.ProfilePictureUrl;
            Messages = mapper.Map<IEnumerable<MessageViewModel>>(user.Messages.OrderByDescending(x => x.CreatedOn));
            PaginatedMessages = PaginatedList<MessageViewModel>.Create(Messages, pageNumber ?? 1, GlobalConstants.PageSize);
            await messageService.MakeAllMessagesOld(user.Id);
            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }
        
        public async Task<IActionResult> OnGetAsync(int? pageNumber)
        {
            var user = await _userManager.Users.Include(x => x.Messages).SingleOrDefaultAsync(x=> x.Id == _userManager.GetUserId(User));
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user, pageNumber);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? pageNumber)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user, pageNumber);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
