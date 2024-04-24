﻿using ASP.NET_Core_MVC.DAL.Data;
using ASP.NET_Core_MVC_03.PL.Services.EmailSender;
using ASP.NET_Core_MVC_03.PL.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ASP.NET_Core_MVC_03.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IConfiguration configuration, IEmailSender emailSender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user is null)
                {
                    user = new ApplicationUser()
                    {
                        FName = model.FirstName,
                        LName = model.FirstName,
                        UserName = model.LastName,
                        Email = model.Email,
                        IsAgree = model.IsAgree,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(SignIn));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }

                ModelState.AddModelError(string.Empty, "This UserName is Taken, Use another.");
            }
            return View(model);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.IsLockedOut)
                            ModelState.AddModelError(string.Empty, "Your Account is Lockes!!");

                        if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        if (result.IsNotAllowed)
                            ModelState.AddModelError(string.Empty, "Your Account is not Confirmed yet!!");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View();
        }

        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { eamil = user.Email, token = resetPasswordToken }/*, "https", "localhost:5001"*/);

                    await _emailSender.SendAsync(
                    from: _configuration["EmailSettings: SenderEmail"],
                    recipients: model.Email,
                    subject: "Reset Your Password",
                    body: resetPasswordUrl);

                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "There is No Account with this Email!!");
            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;

			return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["Email"] as string;
                var token = TempData["Token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    await _userManager.ResetPasswordAsync(user, token, model.NewPassword); 
                    return RedirectToAction(nameof(SignIn));
				}
				ModelState.AddModelError(string.Empty, "Url is not valid");    
            }
			return View(model);
		}
    }
}
