export default {
    methods: {
        setFocusInvalid() {
            const a = document.getElementsByClassName("is-invalid");
            this.$nextTick(() => {
                if (typeof a[0] !== 'undefined') {
                    a[0].focus();
                }
            });
        }
    },
    mounted() {
        const a = document.getElementsByClassName("form-control");
        a[0].focus();
    },
}