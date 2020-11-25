import Vue from 'vue'
import VueRouter from 'vue-router'
import DashBoard from '../views/DashBoard.vue'
import Login from "../views/Login.vue";
import Register from "../views/Register.vue";
import Setting from "../views/Setting.vue";
import store from '@/store'
import allService from "@/services/all";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'DashBoard',
    component: DashBoard,

  },
  {
    path: '/login',
    name: "Login",
    component: Login,
    meta: { allowAnonymous: true }
  },
  {
    path: '/register',
    name: "Register",
    component: Register,
    meta: { allowAnonymous: true }
  },
  {
    path: '/setting',
    name: "Setting",
    component: Setting,
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach(async (to, from, next) => {
  if (to.matched.some(record => record.meta.allowAnonymous)) {
    if(to.path == '/login' && store.state.token){
      next('/');
    }
    next()
  } else {
    var checkAuth = await allService.checkAuth();
    if (!store.state.token || checkAuth.status !== 200) {
      console.log(store)
      next({
        path: '/login'
      })
    }
    else {
      next()
    }
  }
})

export default router
