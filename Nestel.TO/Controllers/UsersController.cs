using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Nestel.TO.Controllers
{
	[Authorize(Roles = "UserViewer")]
	public class UsersController : Controller
	{
		private readonly Models.Context context;
		private readonly UserManager<Models.User> userManager;
		private readonly SignInManager<Models.User> signInManager;

		public UsersController(Models.Context context, UserManager<Models.User> userManager, SignInManager<Models.User> signInManager)
		{
			ViewBag.IsLogged = false;
			this.context = context;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		#region Переход на частичные представления

		[HttpGet]
		public IActionResult List()
		{
			ViewModels.Account.IndexItemViewModel[] model = context.Users.OrderBy(w => w.UserName)
				.Select(w => new ViewModels.Account.IndexItemViewModel
				{
					Id = w.Id,
					Name = w.UserName,
					Roles = string.Join(", ", context.UserRoles
						  .Where(ur => ur.UserId == w.Id)
						  .Select(ur => context.Roles.FirstOrDefault(r => r.Id == ur.RoleId))
						  .Select(r => r.Name)
						  .OrderBy(r => r)
						  .ToList())
				})
				.ToArray();
			foreach (var item in model)
			{
				item.CanRemove = !item.Roles.Split(',').Any(w => w.Contains("UserEditor"));
			}
			return base.PartialView(model);
		}

		[HttpGet]
		[Authorize(Roles = "UserEditor")]
		public IActionResult Create()
		{
			return PartialView(new ViewModels.Users.CreateUserViewModel());
		}

		[HttpPost]
		[Authorize(Roles = "UserEditor")]
		public async Task<IActionResult> Create(ViewModels.Users.CreateUserViewModel model)
		{
			model.Errors.Items.Clear();

			if (model == null)
			{
				model.Errors.Items.Add("Пустой параметр");
				return PartialView(model);
			}
			if (string.IsNullOrEmpty(model.Username))
			{
				model.Errors.Items.Add("Не указано имя пользователя.");
				return PartialView(model);
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				model.Errors.Items.Add("Не указан пароль пользователя.");
				return PartialView(model);
			}
			if (model.Password.Length < 4)
			{
				model.Errors.Items.Add("Пароль должен быть более 3 символов.");
				return PartialView(model);
			}

			var result = await userManager.CreateAsync(new Models.User
			{
				Email = model.EMail,
				UserName = model.Username,
				PhoneNumber = model.Phone
			}, model.Password);

			if (!result.Succeeded)
			{
				foreach (var er in result.Errors)
				{
					if (er.Code == "DuplicateUserName")
						model.Errors.Items.Add($"Пользователь \'{model.Username}\' уже существует");
				}
				return PartialView(model);
			}
			else
				return RedirectToAction("List");
		}

		[HttpGet]
		[Authorize(Roles = "UserEditor")]
		public IActionResult Edit(long id)
		{
			var f = context.Users.Find(id);
			if (f == null)
				return NotFound();
			return View(new ViewModels.Users.EditViewModel
			{
				Id = f.Id,
				EMail = f.Email,
				Username = f.UserName,
				Phone = f.PhoneNumber
			});
		}

		[HttpPost]
		[Authorize(Roles = "UserEditor")]
		public async Task<IActionResult> Edit([FromForm] ViewModels.Users.EditViewModel model)
		{
			try
			{
				model.Errors.Items.Clear();

				if (model == null)
				{
					model.Errors.Items.Add("Пустой параметр");
					return PartialView(model);
				}
				if (string.IsNullOrEmpty(model.Username))
				{
					model.Errors.Items.Add("Не указано имя пользователя.");
					return PartialView(model);
				}

				var entity = await context.Users.FindAsync(model.Id);

				entity.Id = model.Id;
				entity.Email = model.EMail;
				entity.UserName = model.Username;
				entity.PhoneNumber = model.Phone;

				context.Update(entity);
				await context.SaveChangesAsync();


				//var result = await userManager.UpdateAsync(new Models.User
				//{
				//	Id = model.Id,
				//	Email = model.EMail,
				//	UserName = model.Username,
				//	PhoneNumber = model.Phone
				//});

				//if (!result.Succeeded)
				//{
				//	foreach (var er in result.Errors)
				//	{
				//		if (er.Code == "DuplicateUserName")
				//			model.Errors.Items.Add($"Пользователь \'{model.Username}\' уже существует");
				//	}
				//	return PartialView(model);
				//}
			}
			catch (Exception ex)
			{
				model.Errors.Items.Add(ex.Message);
				return PartialView(model);
			}

			return RedirectToAction("List");
		}

		[HttpGet]
		[Authorize(Roles = "UserEditor")]
		public IActionResult Delete(long id)
		{
			var f = context.Users.Find(id);
			if (f == null)
				return NotFound();
			return View(new ViewModels.Users.EditViewModel
			{
				Id = f.Id,
				EMail = f.Email,
				Username = f.UserName,
				Phone = f.PhoneNumber
			});
		}

		[HttpPost(Name = "Delete")]
		[Authorize(Roles ="UserEditor")]
		public IActionResult DeletePost(long id)
		{
			var f = context.Users.Find(id);
			if (f == null)
				return NotFound();
			context.Users.Remove(f);

			return RedirectToAction("List");
		}

		#endregion
	}
}