using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Models
{
	public class Initializer
	{
		public static async Task Initialize(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			if(await roleManager.FindByNameAsync("admin") == null)
			{
				await roleManager.CreateAsync(new Role("admin"));
			}
			if(await roleManager.FindByNameAsync("user") == null)
			{
				await roleManager.CreateAsync(new Role("user"));
			}

			var admin = await userManager.FindByNameAsync("admin");

			if(admin == null)
			{
				var result = await userManager.CreateAsync(admin = new User
				{
					UserName = "admin"
				},
				"_Aa1234567");

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(admin, "admin");
				}
			}
		}
	}
}
