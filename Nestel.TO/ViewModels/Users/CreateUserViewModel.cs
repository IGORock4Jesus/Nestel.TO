using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.ViewModels.Users
{
	public class CreateUserViewModel
	{
		public Errors Errors { get; } = new Errors();

		[Required]
		[Display(Name = "Логин")]
		public string Username { get; set; }
		[Required]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = "Почта")]
		public string EMail { get; set; }
		[Display(Name = "Сотовый")]
		public string Phone { get; set; }
	}
}
