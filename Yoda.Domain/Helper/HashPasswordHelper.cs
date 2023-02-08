using System.Security.Cryptography;
using System.Text;

namespace Yoda.Domain.Helper
{
	public static class HashPasswordHelper
	{
		/// <summary>
		/// Converting password in hash.
		/// </summary>
		/// <param name="password"></param>
		/// <returns>Password hash in SHA256(string).</returns>
		public static string HashPassowrd(string password)
		{
			using var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			return hash;
		}
	}
}
