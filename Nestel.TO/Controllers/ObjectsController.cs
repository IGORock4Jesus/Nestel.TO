using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Nestel.TO.Controllers
{
	[Authorize(Roles = "ObjectViewer")]
	public class ObjectsController : Controller
	{
		private readonly Models.Context context;
		private readonly UserManager<Models.User> userManager;

		public ObjectsController(Models.Context context, UserManager<Models.User> userManager)
		{
			ViewBag.IsLogged = false;
			this.context = context;
			this.userManager = userManager;
		}
		// GET: Objects
		public async Task<ActionResult> Index()
		{
			await this.ShowUser(userManager);

			var roles = await userManager.GetRolesAsync(await userManager.GetUserAsync(HttpContext.User));
			ViewBag.AllowEdit = roles.Any(w => w == "ObjectEditor");
			return View(new ViewModels.ObjectListViewModel
			{
				Objects = context.Objects.OrderBy(w => w.Name)
				.Select(w => new ViewModels.ObjectViewModel { Id = w.Id, Name = w.Name })
				.ToList()
			});
		}

		// GET: Objects/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		[Authorize(Roles = "ObjectEditor")]
		// GET: Objects/Create
		public async Task<ActionResult> Create()
		{
			await this.ShowUser(userManager);
			return View(new ViewModels.CreateObjectViewModel { IsShowSuccess = false });
		}

		// POST: Objects/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "ObjectEditor")]
		public async Task<ActionResult> Create(ViewModels.CreateObjectViewModel viewModel)
		{
			await this.ShowUser(userManager);
			try
			{
				await context.Objects.AddAsync(new Models.Object
				{
					Id = viewModel.Id,
					Name = viewModel.Name
				});
				var result = await context.SaveChangesAsync();
				if (result == 1)
					viewModel.IsShowSuccess = true;
				else
				{
					ModelState.AddModelError("", "Ошибка при создании объекта.");
				}
			}
			catch
			{
			}
			return View(viewModel);
		}

		// GET: Objects/Edit/5
		[Authorize(Roles = "ObjectEditor")]
		public async Task<ActionResult> Edit(long id)
		{
			await this.ShowUser(userManager);
			var obj = await context.Objects.FindAsync(id);
			return View(new ViewModels.EditObjectViewModel
			{
				Id = obj.Id,
				Address = obj.Address,
				Error = null,
				IsShowError = false,
				IsShowSuccess = false,
				Name = obj.Name
			});
		}

		// POST: Objects/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "ObjectEditor")]
		public async Task<ActionResult> Edit(ViewModels.EditObjectViewModel viewModel)
		{
			try
			{
				await this.ShowUser(userManager);
				var model = new Models.Object
				{
					Id = viewModel.Id,
					Address = viewModel.Address,
					Name = viewModel.Name
				};
				context.Entry(model);
				context.Update(model);
				await context.SaveChangesAsync();
			}
			catch
			{
			}
			return View(viewModel);
		}

		// GET: Objects/Delete/5
		[Authorize(Roles = "ObjectEditor")]
		public async Task<IActionResult> Delete(long id)
		{
			await this.ShowUser(userManager);
			return View();
		}

		// POST: Objects/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "ObjectEditor")]
		public async Task<IActionResult> Delete(long id, IFormCollection collection)
		{
			await this.ShowUser(userManager);
			try
			{
				var obj = await context.Objects.FindAsync(id);
				if (obj != null)
				{
					context.Objects.Remove(obj);
					await context.SaveChangesAsync();
				}
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Исключение", ex.Message);
				return RedirectToAction(nameof(Index));
			}
		}
	}
}