
const tokenKey = "x-jwt";
export default {
    setToken(token) {
        window.localStorage.setItem(tokenKey, token);
    },

    removeAllItem() {
        window.localStorage.clear();
    },

    get getToken() {
        var token = window.localStorage.getItem(tokenKey);
        return token;
    }
}