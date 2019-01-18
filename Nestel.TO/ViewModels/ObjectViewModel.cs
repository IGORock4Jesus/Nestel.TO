using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.ViewModels
{
	public class ObjectListViewModel
	{
		public List<ObjectViewModel> Objects { get; set; } = new List<ObjectViewModel>();
		public string Message { get; set; }
	}

	public class ObjectViewModel
	{
		public long Id { get; set; }
		[Display(Name = "Название")]
		public string Name { get; set; }
		[Display(Name = "Адрес")]
		public string Address { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class CreateObjectViewModel : ObjectViewModel
	{
		public bool IsShowSuccess { get; set; }
		//public bool IsShowError { get; set; }
	}

	public class EditObjectViewModel : ObjectViewModel
	{
		public bool IsShowSuccess { get; set; }
		public bool IsShowError { get; set; }
		public string Error { get; set; }
	}
}
