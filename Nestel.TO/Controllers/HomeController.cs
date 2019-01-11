using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nestel.TO.Models;

namespace Nestel.TO.Controllers
{
	[Authorize(Roles = "admin")]
	public class HomeController : Controller
	{
		private readonly Context context;

		public HomeController(Context context)
		{
			this.context = context;
		}

		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		[AllowAnonymous]
		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
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
