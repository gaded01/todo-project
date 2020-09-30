using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Methods;
using server.Models;
using server.Repository.Concrete;

namespace server.Repository.Abstract {
	public class UserRepository : RepositoryBase<User>, IUserRepository {

		private readonly TodoContext _todoContext;
		public UserRepository (TodoContext todoContext) : base (todoContext) {
			
				_todoContext = todoContext;
		}
    public bool ExistUser(User createUser)
    {
      	return _todoContext.Users
          .Any(x => x.Password == EncryptMethod
          .ConvertToEncrypt(createUser.Password));
    }

    public IEnumerable<User> UserActivity()
    {
			return _todoContext.Users
        .Include(a => a.Activities)
        .ToList();
    }

    public User GetSingleUser(int id)
    {
			var user = _todoContext.Users
        .Include(a => a.Activities)
        .Where(i => i.UserId == id)
        .Single();
        return user;
    }

   
  }
}