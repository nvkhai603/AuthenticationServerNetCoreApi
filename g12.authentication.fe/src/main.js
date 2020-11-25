import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './styles/main.scss';
import Vuelidate from 'vuelidate'
import VueToastr from "nvk-toastr";

Vue.use(Vuelidate)
Vue.use(VueToastr, {
  defaultPosition: "toast-bottom-right",
  defaultProgressBar: false
});
Vue.prototype.$enum = {
  SUCCESS: 200,
  EXCEPTION: 1000
}
Vue.config.productionTip = false;

Vue.config.errorHandler = (err, vm, info) => {
  console.log("error", err);
  console.log("vm", vm);
  console.log("info", info);
};

export const app = new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
