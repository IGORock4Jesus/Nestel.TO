using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nestel.TO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Claims
{
	public class RoleManager : RoleManager<Role>
	{
		public RoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
		{
		}

	}
}
