using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Models
{
	/// <summary>
	/// Объект.
	/// </summary>
	public class Object
	{
		public long Id { get; set; }
		[Required]
		[MaxLength(260)]
		public string Name { get; set; }
		public string Address { get; set; }
	}
}
