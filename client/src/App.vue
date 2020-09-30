<template>
  <v-app>
    <v-navigation-drawer
      v-if="currentUser"
      v-model="drawer"
      color="#1F2833"
      app
      dark
      clipped
    >
      <v-list dense>
        <v-list-item link>
          <v-list-item-action>
            <v-icon
              color ="#66FCF1"
            >
              mdi-format-list-bulleted
            </v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Event List</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
         <v-list-item link>
          <v-list-item-action>
            <v-icon
              color ="#66FCF1"
            >
             mdi-calendar-multiple
            </v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Calendar</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item link>
          <v-list-item-action>
            <v-icon
              color ="#66FCF1"
            >
              mdi-logout-variant
            </v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title
              href @click.prevent="logOut"
            >Logout</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar v-if="currentUser"  color="#1F2833" dark app clipped-left>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"
        color ="#66FCF1"
      >
      </v-app-bar-nav-icon>
      <v-toolbar-title>PLANO</v-toolbar-title>
      <v-spacer></v-spacer>
      {{currentUser.name}}
      <v-btn icon>
        <v-icon
          color ="#66FCF1"
        >
          mdi-account-circle
        </v-icon>
      </v-btn>
    </v-app-bar>
    <router-view></router-view>
  </v-app>
</template>

<script>
export default {
  name: 'App',

  data: () => ({
      drawer: null,
  }),

  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    },
  },
  methods: {
    logOut() {
      this.$store.dispatch('auth/logout');
      this.$router.push('/login');
    },
  },
}

</script>
