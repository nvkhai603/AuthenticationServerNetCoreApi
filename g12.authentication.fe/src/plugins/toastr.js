import { app } from '@/main'
export default {
    e(message){
        app.$toastr.e(message);
    },
    s(message){
        app.$toastr.s(message);
    },
    w(message){
        app.$toastr.w(message);
    },
    i(message){
        app.$toastr.i(message);
    }
}