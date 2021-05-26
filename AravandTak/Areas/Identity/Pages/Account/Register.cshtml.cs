using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AravandTak.Data;
using AravandTak.Data.Seeders;
using AravandTak.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AravandTak.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<ArvandTakUser> _signInManager;
		private readonly UserManager<ArvandTakUser> _userManager;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly ApplicationDbContext _dbContext;

		public RegisterModel(
			UserManager<ArvandTakUser> userManager,
			SignInManager<ArvandTakUser> signInManager,
			ApplicationDbContext dbContext,
			ILogger<RegisterModel> logger,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_dbContext = dbContext;
			_logger = logger;
			_emailSender = emailSender;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "نام را وارد کنید")]
			[Display(Name = "نام")]
			public string FirstName { get; set; }

			[Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
			[Display(Name = "نام خانوادگی")]
			public string LastName { get; set; }

			[Required(ErrorMessage = "موبایل را وارد کنید")]
			[Display(Name = "موبایل")]
			public string PhoneNumber { get; set; }

			[Remote("index", "RefferalUserExists", ErrorMessage = "کد معرف معتبر نمی باشد")]
			[Display(Name = "کد معرف")]
			public string RefferalCode { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "{0} باید بین {2} و {1} باشد.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "رمز عبور")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "تکرار رمز")]
			[Compare("Password", ErrorMessage = "رمز و تکرار آن تطابق ندارد")]
			public string ConfirmPassword { get; set; }

			[Required(ErrorMessage = "نشانی خود را وارد کنید")]
			[DataType(DataType.Password)]
			[Display(Name = "نشانی")]
			public string Address { get; set; }
			[Display(Name = "کد پستی")]
			public long? PostalCode { get; set; }

			//public bool AcceptedCondition { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			ReturnUrl = returnUrl;
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl = returnUrl ?? Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			if (ModelState.IsValid)
			{
				var userCode = new Random().Next(100000, 999999).ToString();

				string refferalUserId = null;
				if (!string.IsNullOrEmpty(Input.RefferalCode))
				{
					var refferalUser = await _dbContext.Users.FirstOrDefaultAsync(f => f.Code == Input.RefferalCode);
					if (refferalUser != null) refferalUserId = refferalUser.Id;
				}


				// DB Transation
				using var transaction = _dbContext.Database.BeginTransaction();

				var user = new ArvandTakUser
				{
					FirstName = Input.FirstName,
					LastName = Input.LastName,
					Code = userCode,
					RefferalUserId = refferalUserId,
					PhoneNumber = Input.PhoneNumber,
					UserName = Input.PhoneNumber.ToString(),
				};

				var result = await _userManager.CreateAsync(user, Input.Password);
				if (result.Succeeded)
				{
					// save user role
					await _userManager.AddToRoleAsync(user, MainAdminSeeder.CustomerRoleName);

					// save user address 
					var address = new Address
					{
						Line1 = Input.Address,
						PostalCode = Input.PostalCode, 
						UserId = user.Id
					};

					await _dbContext.Addresses.AddAsync(address);
					await _dbContext.SaveChangesAsync();

					transaction.Commit();
					transaction.Dispose();

					_logger.LogInformation("User created a new account with password.");

					// var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					//code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					//var callbackUrl = Url.Page(
					//    "/Account/ConfirmEmail",
					//    pageHandler: null,
					//    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
					//    protocol: Request.Scheme);

					//await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
					//    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					//if (_userManager.Options.SignIn.RequireConfirmedAccount)
					//{
					//    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
					//}
					//else
					//{
					await _signInManager.SignInAsync(user, isPersistent: false);
					return LocalRedirect(returnUrl);
					//}
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}
