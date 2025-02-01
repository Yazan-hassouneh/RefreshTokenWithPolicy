using RefreshTokensWithPolicy.Models;

namespace RefreshTokensWithPolicy.Repository
{
	public interface IAuthRepo
	{
		Task<AuthModel> RegisterAsync(CreateAccount model);
		Task<AuthModel> GetTokenAsync(LoginAccount model);
		Task<string> AddToRoleAsync(AddToRoleModel model);
		Task<AuthModel> RefreshTokenAsync(string token);
		Task<bool> RevokeTokenAsync(string token);
	}
}
