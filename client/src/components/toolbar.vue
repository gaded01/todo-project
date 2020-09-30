<template>
  <div>
    <v-app-bar color="#1F2833" dark app clipped-left>
      <v-app-bar-nav-icon 
         @click.stop="collapseSideBar()"
         color ="#66FCF1"
      >
      </v-app-bar-nav-icon>
      <v-toolbar-title>PLANO</v-toolbar-title>
      <v-spacer></v-spacer>
       <v-btn icon>
        <v-icon
        color ="#66FCF1"
        >mdi-account-circle</v-icon>
      </v-btn>
    </v-app-bar>
  </div>
</template>


<script>
export default {
  name: "Navbar",

  methods: {
    collapseSideBar: function(){
      this.$emit('collapseSideBar')
    }
  }

};
</script>
