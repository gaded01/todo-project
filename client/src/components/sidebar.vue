<template>
   <div>
    
    
   </div>
   
</template>

<script>


export default {
   name: 'Sidebar',

  
   data: function() {
     return {
     drawer: null,
     }
   },
   methods:{
     collapse: function(){
       this.drawer = !this.drawer
     }

   },
     
}
</script>

<style>

</style>