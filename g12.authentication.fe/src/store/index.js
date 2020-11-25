import Vue from 'vue'
import Vuex from 'vuex'
import tokenManager from "@/helpers/tokenManager";
Vue.use(Vuex)
export default new Vuex.Store({
  state: {
    token: tokenManager.getToken ? tokenManager.getToken : null
  },
  mutations: {
    loginSuccess(state, token){
      state.token = token;
      tokenManager.setToken(token);
    },
    logoutSuccess(state){
      state.token = null;
      tokenManager.removeAllItem();
    }
  },
  actions: {
  },
  modules: {
  }
})
