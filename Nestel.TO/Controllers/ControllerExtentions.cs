using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Controllers
{
	public static class ControllerExtentions
	{
		public static async Task ShowUser(this Controller _this, UserManager<Models.User> userManager)
		{
			var user = await userManager.GetUserAsync(_this.HttpContext.User);
			if (user != null)
			{
				_this.ViewBag.IsLogged = true;
				_this.ViewBag.Username = user.UserName;
			}
			else
			{
				_this.ViewBag.IsLogged = false;
			}
		}
	}
}
