using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Nestel.TO.Controllers
{
    public class UsersController : Controller
    {
		private readonly Models.Context context;
		private readonly UserManager<Models.User> userManager;
		private readonly SignInManager<Models.User> signInManager;

		public UsersController(Models.Context context, UserManager<Models.User> userManager, SignInManager<Models.User> signInManager)
		{
			this.context = context;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        public IActionResult Index()
        {
            return View(context.Users.ToArray());
        }
    }
}