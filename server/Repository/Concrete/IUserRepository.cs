using System;
using System.Collections.Generic;
using server.Models;

namespace server.Repository.Concrete
{
  public interface IUserRepository : IRepositoryBase<User>
  {
    bool ExistUser(User createUser);
    IEnumerable<User> UserActivity();
    User GetSingleUser(int id); 
    
  } 
}