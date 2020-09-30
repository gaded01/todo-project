using System.Linq;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repository.Concrete;

namespace server.Repository.Abstract {
	public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository {

		private readonly TodoContext _todoContext;
		public ActivityRepository (TodoContext todoContext) 
			: base(todoContext) { 
				
		_todoContext = todoContext;
		}

    public void CreateActivity(Activity activity)
    {	
      var userAct = _todoContext.Activities.Add(activity);
    }

		public IQueryable<Activity> GetUserActivity(int id)	{
			var getAct = _todoContext.Activities
										.Where(a => a.User.UserId == id);

			return getAct;
		}
  }
}