import axios from 'axios';

const AUTH_API_URL = 'http://localhost:5001/authentication/user';
const CREATE_API_URL = 'http://localhost:5001/users/register';

//Login method
class AuthService {
  login(user) {
    return axios
      .post(AUTH_API_URL, {
        username: user.username,
        password: user.password
      })
      .then(response => {
        if (response.data.token) {
        localStorage.setItem('user', JSON.stringify(response.data));
        console.log("has token")
        }
        return response.data;
      });
  }
  //Logout method, remove token from local storage
  logout() {
    localStorage.removeItem('user');
  }

  //Sign up method
  register(user) {
    return axios.post(CREATE_API_URL, {
      name: user.name,
      email: user.email,
      mobileNumber: user.mobileNumber,
      birthdate: user.birthdate,
      username: user.username,
      password: user.password

    });
  }
}

export default new AuthService();