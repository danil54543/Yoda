using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoda.Domain.Helper
{
	public static class AgeHelper
	{
		public static int GetAge(DateTime date)
		{
			int age = DateTime.Now.Year - date.Year;
			if (DateTime.Now.Month < date.Month || (DateTime.Now.Month == date.Month && DateTime.Now.Day < date.Day))
			{
				age--;
			}
			return age;
		}
	}
}
