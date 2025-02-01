using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RefreshTokensWithPolicy.CustomePolicies
{
	public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
		{
			if(!context.User.Identity!.IsAuthenticated) return Task.CompletedTask;
			if(!context.User.HasClaim(claim => claim.Type == ClaimTypes.DateOfBirth)) return Task.CompletedTask;

			var age = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.DateOfBirth)!.Value;
			int _age = int.Parse(age);

			if (_age >= 18) context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
