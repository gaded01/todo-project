using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repository;
using server.Repository.Concrete;

namespace server.Controllers {

	[Authorize]
	[ApiController]
	[Route ("[controller]")]
	[EnableCors ("CorsPolicy")]
	public class ActivitiesController : ControllerBase {
		private IRepositoryWrap _repoWrapper;

		private IActivityRepository _activityRepo;

		public ActivitiesController (IRepositoryWrap repositoryWrap, IActivityRepository activityRepo) {

			_repoWrapper = repositoryWrap;
			_activityRepo = activityRepo;

		}

		//Post/Activities/addActivity/{userid}
		[HttpPost("addActivity/{userId}")]
		public ActionResult<Activity> createActivity(int userId, Activity activity) {

			var user = _repoWrapper.User.FindById(userId);

			if(user == null)	{
				return Ok(user);
			}
			activity.User = user;
			_activityRepo.CreateActivity(activity);
			_repoWrapper.Save();

			return Ok(activity);
		}
	
		//Get/Activities/fetchUserActivity/{id}
		[HttpGet("fetchActivity/{userId}")]
		public ActionResult<Activity> getActivity(int userId) {
			
			var getAct = _repoWrapper.Activity.GetUserActivity(userId);

			return Ok(getAct);
		} 

		//Delete/activities/deleteActivity/{id}
		[HttpDelete ("deleteActivity/{id}")]
		public ActionResult<Activity> deleteActivity (int id) {

			var activity = _repoWrapper.Activity.FindById(id);
			if (activity == null) {

				return NotFound();
			}

			_repoWrapper.Activity.Remove(activity);
			_repoWrapper.Save();

			return Ok();
		}
		//Put/activities/updateActivity
		[HttpPut ("updateActivity/{id}")]
		public ActionResult<Activity> UserUpdate (int id, Activity activity) {
			if (id != activity.ActId) {
				return BadRequest();
			}
			_repoWrapper.Activity.Update(activity);

			try {
				_repoWrapper.Save ();
			} catch (DataException /* dex */ ) {
				//Log the error
				ModelState.AddModelError (string.Empty, "Unable to save changes");
			}
			return Ok();
		}
		
		
		

	}
}