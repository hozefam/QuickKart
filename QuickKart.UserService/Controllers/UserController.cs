using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickKart.UserService.Models;
using QuickKart.UserService.Repository;

namespace QuickKart.UserService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository userRepository;

		public UserController(IUserRepository userRepository)
        {
			this.userRepository = userRepository;
		}

        [HttpGet]
		[Route("Login")]
		public ActionResult<Task<bool>> Login([FromQuery] string emailId, [FromQuery] string password)
		{
			try
			{
				var response =  this.userRepository.ValidateCredentials(emailId, password);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(false);
			}
		}

		[HttpPost]
		[Route("RegisterUser")]
		public ActionResult<Task<bool>> RegisterUser([FromBody] Users userObj)
		{
			try
			{
				var response = this.userRepository.RegisterUser(userObj);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(false);
			}
		}

		[HttpGet]
		[Route("ValidateEmail")]
		public ActionResult<Task<bool>> ValidateEmail([FromQuery] string emailId)
		{
			try
			{
				var response = this.userRepository.CheckUser(emailId);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return Ok(false);
			}
		}
	}
}
