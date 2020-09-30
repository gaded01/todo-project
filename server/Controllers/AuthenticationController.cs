using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using server.Models;
using server.Services;

namespace server.Controllers {

	[Route("[controller]")]
	[ApiController]
	[EnableCors("CorsPolicy")]
	public class AuthenticationController : ControllerBase {
		
		private IAuthenticateService _authenticateService;

		public AuthenticationController(IAuthenticateService authenticateService) {
				
				_authenticateService = authenticateService;
		}

		[HttpPost ("user")]
		public IActionResult Post([FromBody] User model) {

			var userAuthenticate = _authenticateService.Authenticate(model.Username, model.Password);

			if (userAuthenticate == null)
				return BadRequest(new { message = "Username or Password is incorrect"});

			return Ok(userAuthenticate);
			
		}
	}
}