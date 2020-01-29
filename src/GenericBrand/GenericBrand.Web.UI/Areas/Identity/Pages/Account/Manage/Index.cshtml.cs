﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GenericBrand.Data.DAL.Identity.Stores;
using GenericBrand.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GenericBrand.Web.UI.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public IndexModel(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Forename")]
            [StringLength(100, ErrorMessage = "{0} must be at least {2} and a maximum of {1} characters long.", MinimumLength = 1)]
            public string Forename { get; set; }

            [Required]
            [Display(Name = "Surname")]
            [StringLength(100, ErrorMessage = "{0} must be at least {2} and a maximum of {1} characters long.", MinimumLength = 1)]
            public string Surname { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var forename = await _userManager.GetFirstNameAsync(user);
            var surname = await _userManager.GetLastNameAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Forename = forename,
                Surname = surname,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // Forename
            var forename = await _userManager.GetFirstNameAsync(user);
            if (Input.Forename != forename)
            {
                var setForename = await _userManager.SetFirstNameAsync(user, Input.Forename);
                if (!setForename.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting forename for user with ID '{userId}'.");
                }
            }

            // Surname
            var surname = await _userManager.GetLastNameAsync(user);
            if (Input.Surname != surname)
            {
                var setSurname = await _userManager.SetLastNameAsync(user, Input.Surname);
                if (!setSurname.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting surname for user with ID '{userId}'.");
                }
            }

            // Phone Number
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
