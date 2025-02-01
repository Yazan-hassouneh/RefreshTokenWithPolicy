using Microsoft.AspNetCore.Identity;

namespace RefreshTokensWithPolicy.Models
{
	public class ApplicationUser : IdentityUser
	{
		public int Age { get; set; }
		public virtual List<RefreshToken>? RefreshTokens { get; set; } = [];
	}
}
