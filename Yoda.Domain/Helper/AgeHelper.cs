namespace Yoda.Domain.Helper
{
	public static class AgeHelper
	{
		/// <summary>
		/// Сalculating age from date.
		/// </summary>
		/// <param name="date">Bird date.</param>
		/// <returns>User age(int).</returns>
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
