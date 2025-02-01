using RefreshTokensWithPolicy.Models;
using Microsoft.AspNetCore.Mvc;
using RefreshTokensWithPolicy.Repository;
using RefreshTokensWithPolicy.Services;

namespace RefreshTokensWithPolicy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController(IAuthRepo authRepo) : ControllerBase
	{
		private readonly IAuthRepo _authRepo = authRepo;
		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create([FromBody]CreateAccount newAccount)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _authRepo.RegisterAsync(newAccount);
			if (!result.IsAuthenticated) return BadRequest(result.Message);
			SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);
			return Ok(result);
		}
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginAccount loginInfo)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _authRepo.GetTokenAsync(loginInfo);
			if (!result.IsAuthenticated) return BadRequest(result.Message);
			if (!string.IsNullOrEmpty(result.RefreshToken)) SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);
			return Ok(result);
		}
		[HttpGet("refreshToken")]
		public async Task<IActionResult> GetRefreshToken()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			var result = await _authRepo.RefreshTokenAsync(refreshToken);
			if (!result.IsAuthenticated) return BadRequest(result);
			SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiration);
			return Ok(result);
		}
		[HttpPost("revoke")]
		public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
		{
			var token = model.Token ?? Request.Cookies["refreshToken"];
			if (string.IsNullOrEmpty(token)) return BadRequest("Token is required");
			var result = await _authRepo.RevokeTokenAsync(token);
			if (!result) return BadRequest("Token is invalid");
			return Ok();
		}
		private void SetRefreshTokenInCookies(string refreshToken, DateTime expires)
		{
			var cookiesOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = expires.ToLocalTime(),
			};
			Response.Cookies.Append("refreshToken", refreshToken, cookiesOptions);
		}
	}
}
