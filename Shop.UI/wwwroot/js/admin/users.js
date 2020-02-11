﻿var app = new Vue({
    el: '#app',
    data: {
        username: ""
    },
    mounted() {
        //TODO: Get All Users
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post('/users', { username: this.username })
                .then(res => {
                    console.log(res);
                })
                .catch(err => {
                    console.log(err);
                });      
        }
    }
})