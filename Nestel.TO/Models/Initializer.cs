using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Models
{
	public class Initializer
	{
		public static async Task Initialize(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			Debug.WriteLine("----------------------------->>>>>>>>>");
			Debug.WriteLine("Database Initializing...");
			Debug.WriteLine("<<<<<<<<<-----------------------------");

			await AddRoleIfNotExist(roleManager, "Guest");
			await AddRoleIfNotExist(roleManager, "AccountViewer");
			await AddRoleIfNotExist(roleManager, "AccountEditor");
			await AddRoleIfNotExist(roleManager, "ObjectViewer");
			await AddRoleIfNotExist(roleManager, "ObjectEditor");
			await AddRoleIfNotExist(roleManager, "ApplicationViewer");
			await AddRoleIfNotExist(roleManager, "ApplicationEditor");

			var result = await AddUserIfNotExist(userManager, "admin", "_Aa1234567", false);

			if (result)
			{
				await AddRoleToUser(userManager, "admin", "Guest");
				await AddRoleToUser(userManager, "admin", "AccountViewer");
				await AddRoleToUser(userManager, "admin", "AccountEditor");
				await AddRoleToUser(userManager, "admin", "ObjectViewer");
				await AddRoleToUser(userManager, "admin", "ObjectEditor");
				await AddRoleToUser(userManager, "admin", "ApplicationViewer");
				await AddRoleToUser(userManager, "admin", "ApplicationEditor");
			}
		}

		private static async Task AddRoleToUser(UserManager<User> userManager, string username, string rolename)
		{
			await userManager.AddToRoleAsync(await userManager.FindByNameAsync(username), rolename);
		}

		private static async Task<bool> AddUserIfNotExist(UserManager<User> userManager, string username, string password, bool removable)
		{
			var admin = await userManager.FindByNameAsync(username);

			if (admin == null)
			{
				var result = await userManager.CreateAsync(admin = new User
				{
					UserName = username,
					Removable = removable
				},
				password);

				return result.Succeeded;
			}

			return false;
		}

		private static async Task AddRoleIfNotExist(RoleManager<Role> roleManager, string name)
		{
			if (await roleManager.FindByNameAsync(name) == null)
			{
				await roleManager.CreateAsync(new Role(name));
			}
		}
	}
}
