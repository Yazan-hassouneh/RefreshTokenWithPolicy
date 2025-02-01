using System.Text.Json.Serialization;

namespace RefreshTokensWithPolicy.Models
{
	public class AuthModel
	{
		public string Message { get; set; } = string.Empty;
		public bool IsAuthenticated { get; set; } = false;
		public string UserName { get; set; } = null!;
		public string UserEmail { get; set; } = null!;
		public List<string> UserRoles { get; set; } = [];
		public string Token { get; set; } = null!;
		//public DateTime ExpiresOn { get; set; }

		[JsonIgnore]
		public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpiration { get; set; }
	}
}
