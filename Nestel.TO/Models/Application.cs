using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.Models
{
	/// <summary>
	/// Заявка.
	/// </summary>
	public class Application
	{
		public long Id { get; set; }
		public DateTime CreateDateTime { get; set; }
		[Required]
		public User CreateUser { get; set; }
		[Required]
		public Object Object { get; set; }
	}
}
