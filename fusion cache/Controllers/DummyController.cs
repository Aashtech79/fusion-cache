using Fusion_Cache_Lib.Services.DummyService;
using Microsoft.AspNetCore.Mvc;

namespace fusion_cache.Controllers
{
    [ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class DummyController : ControllerBase
	{
		private readonly IDummyService _dummyService;

		public DummyController(IDummyService dummyService)
		{
			_dummyService = dummyService;	
		}
		[HttpGet]
		[Route("employees")]
		public async Task <IActionResult> GetEmployees()
		{
			try
			{
				return Ok(await _dummyService.GetEmployees());

			}
			catch (Exception ex) 
			{ 
				return BadRequest(ex.Message);	
			}
		}
	}
}
