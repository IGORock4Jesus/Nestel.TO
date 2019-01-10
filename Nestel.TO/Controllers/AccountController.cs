using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Nestel.TO.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<Models.User> signInManager;

		public AccountController(SignInManager<Models.User> signInManager)
		{
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return View(new ViewModels.LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}