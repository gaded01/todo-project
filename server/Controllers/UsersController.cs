using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using server.Methods;
using server.Models;
using server.Repository;
using server.Repository.Concrete;


namespace server.Controllers {


	
	[Route ("[controller]")]
	[ApiController]
	[EnableCors("CorsPolicy")]
	public class UsersController : ControllerBase {
		private IRepositoryWrap _repoWrapper;
		private readonly IUserRepository _userRepo;

		public UsersController (IRepositoryWrap repoWrapper, IUserRepository userRepo) {
			_repoWrapper = repoWrapper;
			_userRepo = userRepo;
		}

		//GET api/users
		
		[HttpGet]
		
		public ActionResult<IEnumerable<User>> FindAll() {

			var users = _repoWrapper.User.UserActivity();
			return Ok(users);

		}
		
		//GET api/users/{id}
		[Authorize]
		[HttpGet ("{id}")]
		public ActionResult<User> FindById(int id) {
			var user = _repoWrapper.User.GetSingleUser(id);
			return Ok(user);
		}
		

		[HttpPost ("register")]
		public ActionResult<User> RegisterUser(User createUser) {

			var existUser = _repoWrapper.User.ExistUser(createUser);

			if(existUser == true)	{
				return BadRequest(new {message="Username or Password Already Exist!"});
			}
			else{
			createUser.Password = EncryptMethod.ConvertToEncrypt(createUser.Password);
			_repoWrapper.User.Create(createUser);
			_repoWrapper.Save ();

			return Ok(new {message="Successfully Registered!"});
			}

		}	


		[Authorize]
		[HttpDelete ("{id}")]
		public ActionResult<User> RemoveUser (int id) {

			var user = _repoWrapper.User.FindById(id);
			if (user == null) {

				return NotFound();
			}

			_repoWrapper.User.Remove(user);
			_repoWrapper.Save();

			return Ok();
		}
		

		[HttpPut ("{id}")]
		public ActionResult UserUpdate (int id, User user) {
			if (id != user.UserId) {
				return BadRequest();
			}
			_repoWrapper.User.Update(user);

			try {
				_repoWrapper.Save ();
			} catch (DataException /* dex */ ) {
				//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
				ModelState.AddModelError (string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
			}
			return NoContent();
		}

	}
}