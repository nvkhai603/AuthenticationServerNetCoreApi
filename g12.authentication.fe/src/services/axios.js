import axios from 'axios';
import managerHelper from '@/helpers/tokenManager'
// import router from '@/router'
const api = axios.create({
    baseURL: process.env.VUE_APP_BASEURL,
    timeout: 3000,
    validateStatus: function (status) {
        return status >= 200 && status < 600;
    },
})
api.interceptors.request.use(function (config) {
    var newConfig = config;
    if (managerHelper.getToken) {
        newConfig = { ...config, headers: { Authorization: "Bearer " + managerHelper.getToken } }
    }
    return newConfig;
}, function (error) {
    return Promise.reject(error)
})
/*eslint no-unused-vars: "off"*/
// api.interceptors.response.use(function (response) {
//     if (response.status == 401) {
//         managerHelper.removeManager();
//         router.push({ name: "Login" })
//     } else {
//         return response;
//     }

// }, function (error) {
// });
export { api }