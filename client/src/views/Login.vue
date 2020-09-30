<template>
  <div>
		<v-app>
				<v-main>
					<v-container
						class="fill-height"
						fluid
					>
						<v-row
							align="center"
							justify="center"
						>
							<v-col
								col="10"
								sm="8"
								md="4"
							>
								<div class="text-center" id="signup-title">
									<h1>Login Here</h1>
									<h5>New to this app? <router-link to="/SignUp"> Sign up Here</router-link></h5>
								</div>
								<form name="form" @submit.prevent="handleLogin">
									<v-text-field
										v-model="user.username"
										v-validate="'required'"
										type="text"
										label="Name"
										name="username"
									/>    
									
									<v-text-field
										v-model="user.password"
										v-validate="'required'"
										label="Password"
										name="password"
										type="password"
									/>
									<div class="text-center">
										<v-btn
											color="#1F2833"
											dark
											rounded
											type="submit"
										>
										Sign up
										</v-btn><br>
										<v-progress-circular
												v-show="loading"
												indeterminate
												color="primary"
										>
										</v-progress-circular>
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
import User from '../models/user';

export default {
	name: "Login",
	data() {
		return {
			user: new User('',''),
			loading: false,
			message: ''
		};
	},

	computed: {
		loggedIn() {
			return this.$store.state.auth.status.loggedIn;
		}
	},
	created() {
		if(this.loggedIn) {
			this.$router.push('/Home');
		}
	},
	methods: {
		handleLogin() {	
			this.loading = true;
			this.$validator.validateAll().then(isValid => {
				if(!isValid) {
					this.loading = false;
					return;
				}
				if(this.user.username && this.user.password) {
					this.$store.dispatch('auth/login', this.user).then(
						() => {
							this.$router.push('/home');	
						},
						error => {
							this.loading = false;
							this.message = 
								(error.response && error.response.data) ||
								error.message ||
								error.toString();
								console.log(this.message);
						}
					)
				}
			});
		}
	}
};
</script>

<style scoped>
#signup-title {
  margin: -100px 0px 50px 0px;
}

</style>