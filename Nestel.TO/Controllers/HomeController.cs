using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nestel.TO.Models;

namespace Nestel.TO.Controllers
{
	[Authorize(Roles = "Guest")]
	public class HomeController : Controller
	{
		private readonly Context context;
		private readonly UserManager<User> userManager;

		public HomeController(Context context, UserManager<User> userManager)
		{
			ViewBag.IsLogged = false;
			this.context = context;
			this.userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			await this.ShowUser(userManager);
			var user = await userManager.GetUserAsync(HttpContext.User);
			ViewModels.LayoutViewModel viewModel = new ViewModels.LayoutViewModel
			{
				IsShowObjects = await userManager.IsInRoleAsync(user, "ObjectViewer"),
				IsShowUsers = await userManager.IsInRoleAsync(user, "UserViewer")
			};
			return View(viewModel);
		}

		public async Task<IActionResult> About()
		{
			await this.ShowUser(userManager);
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public async Task<IActionResult>  Contact()
		{
			await this.ShowUser(userManager);
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public async Task<IActionResult>  Privacy()
		{
			await this.ShowUser(userManager);
			return View();
		}

		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
