using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefreshTokensWithPolicy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DataController : ControllerBase
	{
		[HttpGet]
		[Route("products")]
		[Authorize]
		public async Task<IActionResult> Products()
		{
			var data = new 
			{ 
				product = "product",
				message = "Hallow From Data Controller"
			};
			return Ok(data);
		}		
		[HttpGet]
		[Route("users")]
		[Authorize(Policy = "AdminManagerPolicy")]
		public async Task<IActionResult> Users()
		{
			var data = new 
			{
				Users = "Users",
				message = "AdminManagerPolicy"
			};
			return Ok(data);
		}		
		[HttpGet]
		[Route("sales")]
		[Authorize(Policy = "AdminUserPolicy")]
		public async Task<IActionResult> Sales()
		{
			var data = new 
			{
				sales = "sales",
				message = "AdminUserPolicy"
			};
			return Ok(data);
		}
	}
}
