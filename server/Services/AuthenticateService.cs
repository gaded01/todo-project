using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Methods;
using server.Models;

namespace server.Services {
	public class AuthenticateService : IAuthenticateService {
		private readonly TodoContext _todoContext;
		private readonly AppSettings _appSettings;

		public AuthenticateService (TodoContext todoContext, IOptions<AppSettings> appSettings) {

			_todoContext = todoContext;
			_appSettings = appSettings.Value;
		}

		public User Authenticate(string userName, string password) {

			var userAuthenticate = _todoContext.Users.SingleOrDefault(x => x.Username == userName && x.Password == EncryptMethod.ConvertToEncrypt(password));

			//if user return is not found 
			if(userAuthenticate == null) 
				return null;

			//if user is found 
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Key);
			var tokenDescriptor = new SecurityTokenDescriptor {

					Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
						new Claim(ClaimTypes.Name, userAuthenticate.UserId.ToString())
					}),

					Expires = DateTime.UtcNow.AddDays(100),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			
			var token = tokenHandler.CreateToken(tokenDescriptor);
			userAuthenticate.Token = tokenHandler.WriteToken(token);

			userAuthenticate.Password = null;

			return userAuthenticate;
		}
	}
}