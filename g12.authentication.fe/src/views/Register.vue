<template>
  <div
    class="bg-primary auth-wrapper d-flex align-items-center justify-content-center p-3"
  >
    <div class="col-sm-3 mw-500 bg-white py-4 rounded-lg">
      <h3 class="text-center mb-3">ĐĂNG KÝ</h3>
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
        <div v-html="errMessage">

        </div>
      </div>
      <form @submit.prevent="submitFormLogin">
        <input
          type="text"
          placeholder="Tài khoản"
          class="form-control mb-3"
          v-model.trim="$v.userName.$model"
          :class="{ 'is-invalid': $v.userName.$error }"
        />
        <input
          type="text"
          placeholder="Họ và tên"
          class="form-control mb-3"
          v-model.trim="$v.fullName.$model"
          :class="{ 'is-invalid': $v.fullName.$error }"
        />
        <input
          type="text"
          placeholder="Email"
          class="form-control mb-3"
          v-model.trim="$v.email.$model"
          :class="{ 'is-invalid': $v.email.$error }"
        />
        <input
          type="password"
          placeholder="Mật khẩu"
          class="form-control mb-3"
          v-model.trim="$v.password.$model"
          :class="{ 'is-invalid': $v.password.$error }"
        />
        <input
          type="password"
          placeholder="Nhập lại mật khẩu"
          class="form-control mb-3"
          v-model.trim="$v.rePassword.$model"
          :class="{ 'is-invalid': $v.rePassword.$error }"
        />
        <multiselect
          v-model="value"
          label="name"
          :options="groups"
          placeholder="Chọn ứng dụng"
        ></multiselect>
        <button type="submit" class="btn my-3 btn-primary w-100">
          Đăng ký
        </button>
        <div class="text-right">
          Đã có tài khoản? <router-link to="/login">Đăng nhập</router-link>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
// import authService from "@/services/auth";
import { required, minLength, sameAs, email } from "vuelidate/lib/validators";
import groupService from "@/services/groups";
import allService from "@/services/all";
import Multiselect from "@/components/common/mulitiSelect";
import focusMixins from "@/mixins/focus";
export default {
  name: "Register",
  data() {
    return {
      errMessage: "",
      userName: "",
      email: "",
      password: "",
      rePassword: "",
      groups: [],
      value: { id: 1, name: "GROUP12", code: "GROUP12" },
      fullName: ""
    };
  },
  mixins: [focusMixins],
  validations: {
    userName: { required, minLength: minLength(8) },
    password: { required, minLength: minLength(8) },
    email: { required, email },
    rePassword: { sameAsPassword: sameAs('password') },
    fullName: { required, minLength: minLength(8) }
  },

  components: {
    Multiselect,
  },

  methods: {
    /**
     * Gửi form đăng ký
     * Created by nvkhai 25.11.2020
     */
    async submitFormLogin() {
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.errMessage = `Vui lòng nhập đầy đủ thông tin: <br>- Họ tên không được để trống <br>- Tài khoản và mật khẩu lớn hơn 8 ký tự<br> - Email phải có định dạng example@abc.xyz <br>- Nhập lại mật khẩu chưa chính xác`;
        this.setFocusInvalid();
      } else {
         const res = await allService.registerUser({ userName: this.userName, email: this.email, password: this.password, groupCode: this.value.code, name: this.fullName});
        if (res.data.code === this.$enum.SUCCESS) {
          this.$toastr.s("Đăng ký thành công.");
          this.$router.push("/login");
        } else {
          this.errMessage = res.data.message;
        }
      }
    },
    /**
     * Lấy về dữ liệu common
     * Cretad by nvkhai 25.11.2020
     */
    fetch() {
      groupService
        .getAllGroups()
        .then((result) => {
          this.groups = result.data.data;
        })
        .catch((err) => {
          console.log(err);
          this.groups = [];
        });
    },
  },
  created() {
    this.fetch();
  },
};
</script>
<style lang="scss" scoped>
.auth-wrapper {
  height: 100vh;
}
</style>