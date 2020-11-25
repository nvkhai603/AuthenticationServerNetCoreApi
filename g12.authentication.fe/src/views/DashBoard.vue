<template>
  <section class="main-section d-flex justify-content-center align-items-center">
      <div>
          <!-- <div>
              <input type="text" class="w-50 form-control">
          </div> -->
          <div class="list-sub-app row">
          <div class="sub-app mx-3 my-2" title="ứng dụng 1">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="https://startbootstrap.com/assets/img/sb-logo.svg">
              </div>
          </div>
          <div class="sub-app mx-3 my-2">
              <div class="text-center">
                  <img src="@/assets/image/settings.png" width="80" height="80">
              </div>
          </div>
      </div>
      </div>
  </section>
</template>

<script>
export default {
    name: "Dashboard"
}
</script>

<style lang="scss">
/* Set a background image by replacing the URL below */
.main-section {
  height: 100vh;
  background: url('https://source.unsplash.com/twukN12EN7c/1920x1080') no-repeat center center fixed;
  -webkit-background-size: cover;
  -moz-background-size: cover;
  background-size: cover;
  -o-background-size: cover;
}
.sub-app{
    cursor: pointer;
    border-radius: 5px;
    background: rgba(255, 255, 255, 0.6);
    padding: 10px;
    width: 100px;
    height: 100px;
    &:hover{
        background: rgba(255, 255, 255);
    }
}
.list-sub-app{
    max-width: 800px;
}
</style>