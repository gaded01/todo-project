using System;
using System.Text;

namespace server.Methods {
	public static class EncryptMethod {
		public static string PasswordKey = "Iam the password key";

		public static string ConvertToEncrypt (string password) {

			if (string.IsNullOrEmpty (password)) return "";
			password += PasswordKey;
			var passwordBytes = Encoding.UTF8.GetBytes (password);
			return Convert.ToBase64String (passwordBytes);

		}

		public static string ConvertToDecrypt (string base64EncodeData) {

			if (string.IsNullOrEmpty(base64EncodeData)) return "";
			var base64EncodeDataBytes = Convert.FromBase64String(base64EncodeData);
			var result = Encoding.UTF8.GetString(base64EncodeDataBytes);
			result = result.Substring(0, result.Length - PasswordKey.Length);
			return result;
		}

	}
}