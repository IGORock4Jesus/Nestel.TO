using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Models
{
	public class User : IdentityUser<long>
	{
		/// <summary>
		/// Может ли быть удалено.
		/// </summary>
		public bool Removable { get; set; }
	}
}
