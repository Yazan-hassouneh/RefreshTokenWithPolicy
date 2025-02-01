using Microsoft.AspNetCore.Authorization;

namespace RefreshTokensWithPolicy.CustomePolicies
{
	public class MinimumAgeRequirement(int age) : IAuthorizationRequirement
	{

	}
}
