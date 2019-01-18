using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestel.TO.ViewModels.Account
{
	public class IndexViewModel
	{
		public List<IndexItemViewModel> Items { get; set; }
		public bool CanEdit { get; set; }
	}

	public class IndexItemViewModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Roles { get; set; }
		public bool CanRemove { get; set; }
	}
}
