using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.ViewModels.Users
{
	public class EditViewModel
	{
		public long Id { get; set; }
		public string Username { get; set; }
		public string EMail { get; set; }
		public string Phone { get; set; }

		public Errors Errors { get; } = new Errors();

	}
}
