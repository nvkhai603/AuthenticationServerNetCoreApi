<template>
  <div id="app">
    <div class="main">
      <div v-if="isAuthenticated">
        <h1>Đã xác thực</h1>
        <h1>Request tới API và được phản hồi</h1>
        <pre>
          <code> {{reqToApi}} </code>
        </pre>
      </div>
      <div v-else>
        <h1>Chưa xác thực</h1>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "App",
  data() {
    return {
      sid: "",
      isAuthenticated: false,
      reqToApi: "",
    };
  },
  components: {},
  methods: {
    /**
     * Hàm demo gọi đến backend cần có xác thực
     * Created by nvkhai 29.11.2020
     */
    callToBackendWithToken() {
      var token = window.localStorage.getItem("x-token");
      var reqContentDemo = {
        method: "get",
        url: "http://apigateway.toedu.me/demo/",
        headers: {
          Authorization: token,
          "Content-Type": "application/json",
        },
      };
      axios(reqContentDemo)
        .then((result) => {
          this.isAuthenticated = true;
          this.reqToApi = JSON.stringify(result.data, undefined, 4); 
        })
        .catch((err2) => {
          console.log(err2);
        });
    },
    /**
     * Hàm kiểm tra có phải được redirect từ Main App hay không
     * true: đúng, false: sai
     * Created by nvkhai 29.11.2020
     */
    isRedirectedFromMainaApp() {
      var url = window.location;
      this.sid = new URLSearchParams(url.search).get("sid");
      if (
        url.pathname === "/redirect" &&
        this.sid !== null &&
        this.sid !== ""
      ) {
        return true;
      }
      return false;
    },
  },
  // Hàm chạy đầu tiên khi khởi chạy app
  created() {
    if (this.isRedirectedFromMainaApp()) {
      // clear localStorage
      window.localStorage.clear();
      // lấy về Token từ sid
      axios
        .get(
          `http://apigateway.toedu.me/auth/api/intergrates/token?sid=${this.sid}`
        )
        .then((result) => {
          if (result.data.code == 401) {
            // sid không hợp lệ
            this.isAuthenticated = false;
            window.location.href = "http://toedu.me/";
          } else if (result.data.code == 200) {
            this.isAuthenticated = true;
            // Lấy token Thành công
            // Lưu token vào local storage
            window.localStorage.setItem("x-token", result.data.data.token);
            // Tiếp tục những tác vụ khác của app
            this.callToBackendWithToken();
          } else {
            // Lỗi khác
            window.location.href = "http://toedu.me/";
          }
        })
        .catch((err) => {
          // Có lỗi xảy ra
          console.log(err);
          window.location.href = "http://toedu.me/";
        });
    } else {
      // Không phải redirect từ Main App, kiểm tra có token không
      var token = window.localStorage.getItem("x-token");
      if (token !== null && typeof token !== undefined && token !== "") {
        // Có token
        // Gửi request check Token
        var reqContent = {
          method: "get",
          url: "http://apigateway.toedu.me/auth/api/intergrates/authen",
          headers: {
            Authorization: token,
            "Content-Type": "application/json",
          },
        };
        axios(reqContent)
          .then((result) => {
            if (result.status == 200) {
              // Token hợp lệ
              this.isAuthenticated = true;
              // Gọi tiếp Req đến backend demo
              this.callToBackendWithToken();
            } else {
              // Redirect qua main app
              window.location.href = "http://toedu.me/";
            }
          })
          .catch((err) => {
            // Có lỗi xảy ra
            this.isAuthenticated = true;
            console.log(err);
            window.location.href = "http://toedu.me/";
          });
      } else {
        // Trường hợp không có token, redirect qua main app
        window.location.href = "http://toedu.me/";
      }
    }
  },
};
</script>
<style>
.main {
  width: 800px;
  margin: 0 auto;
}
</style>
