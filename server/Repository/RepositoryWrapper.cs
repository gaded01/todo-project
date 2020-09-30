using server.Data;
using server.Repository.Abstract;
using server.Repository.Concrete;

namespace server.Repository {
	public class RepositoryWrapper : IRepositoryWrap {
		private TodoContext _context;
		private IUserRepository _user;
		private IActivityRepository _activity;

		public IUserRepository User {
			get { 
				if (_user == null) {
					_user = new UserRepository (_context);
				}
				return _user;
			}
		}
		public IActivityRepository Activity {
			get {
				if (_activity == null) {
					_activity = new ActivityRepository(_context);
				}
				return _activity;
			}
		}
		public RepositoryWrapper(TodoContext todoContext)
        {
            _context = todoContext;
        }

    public void Dispose()
    {
      throw new System.NotImplementedException();
    }

    public void Save () {
			_context.SaveChanges();
		}
	}
}