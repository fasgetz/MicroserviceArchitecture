<template>
    <div>
        <h3>Добавление пользователя</h3>
        <form @submit.prevent="addAccount">
            <h5>Account</h5>
            <div class="form-group">
                <label>Name</label>
                <input type="text" v-model="name" class="form-control" placeholder="Введите имя">
            </div>
            <div class="form-group">
                <label>Email</label>
                <input type="email" v-model="email" class="form-control" placeholder="Введите email">
            </div>
             <!--error message--> 
            <div class="text-danger" v-for="(error, index) in accountErrors" :key="index">
                <div class="error-msg">{{ error[0] }}</div>
            </div>
            <div class="row m-0 justify-content-end ">
                <div class="p-3">
                    <button class="btn btn-primary" v-on:click="close">Отменить</button>
                </div>
                <div class="p-3">
                    <button type="submit" class="btn btn-success">Добавить</button>
                </div>
            </div>
        </form>
    </div>
</template>
<script>
    import axios from 'axios'

    export default {
        name: "addcomponent",

        data() {
            return {
                accountErrors: null,
                name: null,
                email: null
            }
        },
        computed: {

        },
        methods: {
            // Добавление аккаунта
            addAccount() {

                let obj = {
                    name: this.name,
                    email: this.email
                }

                axios.post
                    ('https://blog.learn-courses.ru/gateway/account', obj)
                    .then(response => {
                        // Передаем в компонент родителя действие и итем, чтобы уведомить о том, что успешное обновление данных
                        this.$emit('added-account', response.data)


                    }).catch(error => {
                        this.accountErrors = error.response.data.errors

                        console.log(error.response.data.errors)
                    });
            },
            close() {
                // destroy the vue listeners, etc
                this.$emit('close-component')

                //// remove the element from the DOM
                //this.$el.parentNode.removeChild(this.$el);
            }
        },
        mounted() {

        },
    }
</script>
