using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nestel.TO
{
	public static class Security
	{
		public static string Crypt(this string _this)
		{
			using (SHA1 sha = SHA1.Create())
			{
				var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(_this));
				return string.Join("", hash.Select(w => w.ToString("x2")));
			}
		}
	}
}
