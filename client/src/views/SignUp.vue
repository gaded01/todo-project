<template>
  <div>
    <v-app>
      <v-main>
        <v-container class="fill-height" fluid>
          <v-row align="center" justify="center">
            <v-col cols="10" sm="5" md="3">
              <div class="text-center" id="signup-title">
                <h1>Get Started</h1>
                <h5>Sign up now and start planning your activities</h5>
              </div>
              <v-alert v-if="message" :type="successful ? 'success' : 'error'">{{message}}</v-alert>
              <form name="form" @submit.prevent="handleRegister">
                <div v-if="!successful">
                  <v-text-field
                    v-model="user.name"
                    v-validate="'required|min:2|max:20'"
                    type="error"
                    label="Name"
                    name="name"
                  />
                  <v-alert
                    v-if="submitted && errors.has('name')"
                    type="error"
                  >{{errors.first('name')}}</v-alert>
                  <v-text-field
                    v-model="user.email"
                    v-validate="'required|email|max:50'"
                    type="error"
                    label="Email"
                    name="email"
                  />
                  <v-alert
                    v-if="submitted && errors.has('email')"
                    type="error"
                  >{{errors.first('email')}}</v-alert>
                  <v-text-field
                    v-model="user.mobileNumber"
                    v-validate="'required|min:10|max:20'"
                    type="text"
                    label="Mobile Number"
                    name="mobileNumber"
                  />
                  <v-alert
                    v-if="submitted && errors.has('mobileNumber')"
                    type="error"
                  >{{errors.first('name')}}</v-alert>
                  <v-text-field
                    v-model="user.birthdate"
                    v-validate="'required|max:20'"
                    type="date"
                    label="Birthdate"
                    name="birthdate"
                  />
                  <v-alert
                    v-if="submitted && errors.has('birthday')"
                    type="error"
                  >{{errors.first('birthday')}}</v-alert>
                  <v-text-field
                    v-model="user.username"
                    v-validate="'required|min:4|max:20'"
                    type="text"
                    label="Username"
                    name="username"
                  />
                  <v-alert
                    v-if="submitted && errors.has('username')"
                    type="error"
                  >{{errors.first('username')}}</v-alert>
                  <v-text-field
                    v-model="user.password"
                    v-validate="'required|min:4|max:40'"
                    type="password"
                    label="Password"
                    name="password"
                  />
                  <v-alert
                    v-if="submitted && errors.has('password')"
                    type="error"
                  >{{errors.first('password')}}</v-alert>
                  <div class="text-center" id="form-footer">
                    <v-btn type="submit" color="primary" dark rounded>Sign up</v-btn>
                    <h5>
                      Already have an account?
                      <router-link to="/Login">Login Here</router-link>
                    </h5>
                  </div>
                </div>
              </form>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </v-app>
  </div>
</template>

<script>
import User from "../models/user";

export default {
  name: "Register",

  components: {},

  data() {
    return {
      user: new User("", "", "", "", "", ""),
      submitted: false,
      successful: false,
      message : '',
    };
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  },
  mounted() {
    if (this.loggedIn) {
      this.$router.push("/Login");
    }
  },
  methods: {
    handleRegister() {
      this.message = "";
      this.submitted = true;
      this.$validator.validate().then((isValid) => {
        if (isValid) {
          this.$store.dispatch("auth/register", this.user).then(
            (data) => {
              this.message = data.message;
              this.successful = true;
            },
            (error) => {
              this.successful = false;
              this.message =
                error.response.data.message
            }
          );
        }
      });
    },
  },
};
</script>

<style scoped>
#signup-title {
  margin: 0px 0px 20px 0px;
}

#form-footer {
  margin-top: 30px;
}

.v-alert {
  font-size: 0.7em;
}

.v-text-field {
  font-size: 0.8em;
  height: 50px;
}
</style>
