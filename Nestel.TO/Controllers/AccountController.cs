using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Nestel.TO.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<Models.User> signInManager;

		public AccountController(SignInManager<Models.User> signInManager)
		{
			ViewBag.IsLogged = false;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult AccessDenied(string ReturnUrl = null)
		{
			return View(new ViewModels.Account.AccessDeniedViewModel { ReturnUrl = ReturnUrl });
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return View(new ViewModels.LoginViewModel { ReturnUrl = returnUrl });
		}

		[AllowAnonymous]
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(ViewModels.LoginViewModel login)
		{
			if (ModelState.IsValid)
			{
				var pass = login.Password/*.Crypt()*/;
				var result = await signInManager.PasswordSignInAsync(login.Username, pass, login.RememberMe, false);

				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl))
					{
						return Redirect(login.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "Неправильные имя пользователя и (или) пароль.");
				}
			}

			return View(login);
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}