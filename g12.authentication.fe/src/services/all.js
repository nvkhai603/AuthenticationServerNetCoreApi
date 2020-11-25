import { api } from './axios'
export default {
    registerUser(objectRegister) {
        return api.post('register', objectRegister);
    },
    basicAuth(userName, password){
        return api.post('basicAuth', {userName: userName, password: password});
    },
    checkAuth(){
        return api.get('authen');
    }
}