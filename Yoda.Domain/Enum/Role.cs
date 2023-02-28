using System.ComponentModel.DataAnnotations;

namespace Yoda.Domain.Enum
{
	/// <summary>
	/// User role.
	/// </summary>
	public enum Role
	{
		[Display(Name = "Admin")]	Admin = 0,
		[Display(Name = "Moderator")]	Moderator = 1,
        [Display(Name = "User")]	User = 2,
	}
}
