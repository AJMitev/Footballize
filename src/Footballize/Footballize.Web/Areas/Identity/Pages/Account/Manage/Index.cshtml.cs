using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Footballize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Footballize.Web.Areas.Identity.Pages.Account.Manage
{
    using System.IO;
    using Attributes;
    using Data.Repositories;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _environment;
        private readonly IRepository<ProfilePicture> _picturesRepository;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IHostingEnvironment environment,
            IRepository<ProfilePicture> picturesRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _environment = environment;
            this._picturesRepository = picturesRepository;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string ProfilePicturePath { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Upload)]
            [MaxFileSize(5 * 1024 * 1024)]
            [AllowedExtensions(new[] { ".jpg", ".png" })]
            public IFormFile ProfilePicture { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var profilePicture = _picturesRepository.All().SingleOrDefault(x => x.UserId == user.Id);
            var profilePicturePath = profilePicture?.PathToFile ??
                                     "http://style.anu.edu.au/_anu/4/images/placeholders/person_8x10.png";

            Username = userName;
            ProfilePicturePath = profilePicturePath;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber

            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
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


            if (Input.ProfilePicture != null)
            {
                var profilePicture = _picturesRepository.All().SingleOrDefault(x => x.UserId == user.Id);

                if (profilePicture != null)
                {
                    _picturesRepository.Delete(profilePicture);
                    await _picturesRepository.SaveChangesAsync();
                }

                await SaveProfilePicture(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task SaveProfilePicture(User user)
        {
            var folderPath = this._environment.WebRootPath + "/img/profiles/";
            Directory.CreateDirectory(folderPath);

            using (var stream = System.IO.File.OpenWrite(_environment.WebRootPath + $"/img/profiles/{user.Id}.jpg"))
            {
                await Input.ProfilePicture.CopyToAsync(stream);
            }


            var entity = new ProfilePicture
            {
                Name = Input.ProfilePicture.FileName,
                ContentType = Input.ProfilePicture.ContentType,
                Size = Input.ProfilePicture.Length,
                PathToFile = $"/img/profiles/{user.Id}.jpg",
                UserId = user.Id
            };

            await _picturesRepository.AddAsync(entity);
            await _picturesRepository.SaveChangesAsync();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
