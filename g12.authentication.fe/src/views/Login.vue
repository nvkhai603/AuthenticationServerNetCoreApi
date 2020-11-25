<template>
  <div
    class="bg-primary auth-wrapper d-flex align-items-center justify-content-center p-3"
  >
    <div class="col-sm-3 mw-500 bg-white py-4 rounded-lg">
      <h3 class="text-center mb-3">ĐĂNG NHẬP</h3>
      <div
        v-if="errMessage !== ''"
        class="alert alert-danger alert-dismissible fade show"
        role="alert"
      >
        <button
          @click="errMessage = ''"
          type="button"
          class="close"
          data-dismiss="alert"
          aria-label="Close"
        >
          <span aria-hidden="true">&times;</span>
        </button>
        {{ errMessage }}
      </div>
      <form @submit.prevent="submitFormLogin">
        <input
          type="text"
          placeholder="Tài khoản"
          class="form-control mb-3"
          v-model.trim="userName"
        />
        <input
          type="password"
          placeholder="Mật khẩu"
          class="form-control mb-3"
          v-model.trim="password"
        />
        <button type="submit" class="btn btn-primary w-100">Đăng nhập</button>
        <div class="text-right mt-3">
          Chưa có tài khoản? <router-link to="/register">Đăng ký</router-link>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
// import authService from "@/services/auth";
import { required } from "vuelidate/lib/validators";
import focusMixins from "@/mixins/focus";
import allService from "@/services/all";
export default {
  name: "Login",
  data() {
    return {
      errMessage: "",
      userName: "",
      password: "",
    };
  },
  mixins: [focusMixins],
  validations: {
    userName: { required },
    password: { required },
  },

  methods: {
    async submitFormLogin() {
      if (this.$v.$invalid) {
        this.errMessage = "Vui lòng nhập đầy đủ thông tin.";
      } else {
        const res = await allService.basicAuth(this.userName, this.password);
        if (res.data.code === this.$enum.SUCCESS) {
          this.$toastr.s("Đăng nhập thành công.");
          this.$store.commit('loginSuccess', res.data.data.token);
          this.$router.push("/");
        } else {
          this.errMessage = res.data.message;
        }
      }
    },
  },
};
</script>
<style lang="scss" scoped>
.auth-wrapper {
  height: 100vh;
}
</style>