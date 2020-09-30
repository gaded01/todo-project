using System.Collections.Generic;
using System.Linq;
using server.Models;

namespace server.Repository.Concrete
{
    public interface IActivityRepository : IRepositoryBase<Activity>
    {
      void CreateActivity(Activity activity);
      IQueryable<Activity> GetUserActivity(int id);
  }
}     

