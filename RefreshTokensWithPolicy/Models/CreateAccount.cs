namespace RefreshTokensWithPolicy.Models
{
	public class CreateAccount
	{
		public string UserName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public int Age { get; set; }
	}
}
