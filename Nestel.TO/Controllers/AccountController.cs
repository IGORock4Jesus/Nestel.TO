using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
		//private async Task Authenticate(Models.User user)
		//{
		//	// создаем один claim
		//	var claims = new List<Claim>
		//	{
		//		new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
		//		new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
		//	};
		//	// создаем объект ClaimsIdentity
		//	ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
		//		ClaimsIdentity.DefaultRoleClaimType);
		//	// установка аутентификационных куки
		//	await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		//}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}