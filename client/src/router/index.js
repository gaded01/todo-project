import Vue from 'vue'
import VueRouter from 'vue-router'
//import Index from '../views/Index.vue'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/SignUp',
    name: 'SignUp',
    component: () => import(/* webpackChunkName: "login" */ '../views/SignUp.vue')
  },
  {
    path: '/Login',
    name: 'Login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "login" */ '../views/Login.vue')
  },
  {
    path: '/Home',
    name: 'Home',
    component: () => import('../views/Home.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})


export default router
