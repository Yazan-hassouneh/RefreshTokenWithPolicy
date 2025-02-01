using Microsoft.AspNetCore.Mvc;
using RefreshTokensWithPolicy.Models;
using RefreshTokensWithPolicy.Repository;

namespace RefreshTokensWithPolicy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController(IAuthRepo authRepo) : ControllerBase
	{
		private readonly IAuthRepo _authRepo = authRepo;

		[HttpPost("addToRole")]
		public async Task<IActionResult> AddToRoleAsync([FromBody] AddToRoleModel model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _authRepo.AddToRoleAsync(model);
			if (!string.IsNullOrEmpty(result)) return BadRequest(result);
			return Ok(model);
		}
	}
}
