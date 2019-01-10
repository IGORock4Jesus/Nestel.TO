using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Models
{
	public class Role : IdentityRole<long>
	{
		public Role()
		{
		}

		public Role(string roleName) : base(roleName)
		{
		}
	}
}
