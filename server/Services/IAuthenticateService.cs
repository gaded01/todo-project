using server.Models;

namespace server.Services {
	public interface IAuthenticateService {

		User Authenticate(string userName, string password);

	}
}