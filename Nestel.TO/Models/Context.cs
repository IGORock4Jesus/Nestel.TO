using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nestel.TO.ViewModels;
using Nestel.TO.ViewModels.Users;
using Nestel.TO.ViewModels.Account;

namespace Nestel.TO.Models
{
	public class Context : IdentityDbContext<User, Role, long>
	{
		public Context([NotNull] DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Object> Objects { get; set; }


	}
}
