<template>
    <div class="container">
        <h3>Редактирование пользователя</h3>
        <form @submit.prevent="saveAccount">
            <h5>Account</h5>
            <div class="form-group">
                <label>Name</label>
                <input type="text" v-model="item.name" class="form-control" placeholder="Введите имя">
            </div>
            <div class="form-group">
                <label>Email</label>
                <input type="email" v-model="item.email" class="form-control" placeholder="Введите email">
            </div>
            <!-- error message -->
            <div class="text-danger" v-for="(error, index) in accountErrors" :key="index">
                <div class="error-msg">{{ error[0] }}</div>
            </div>
            <div class="row m-0 justify-content-end ">
                <button type="submit" class="btn btn-success">Сохранить</button>
            </div>
        </form>
        <form class="mt-3" @submit.prevent="saveProfile">
            <h5>Profile</h5>
            <div class="form-group">
                <label>firstName</label>
                <input type="text" v-model="item.profile.firstName" class="form-control" placeholder="Введите firstName">
            </div>
            <div class="form-group">
                <label>lastName</label>
                <input type="text" v-model="item.profile.lastName" class="form-control" placeholder="Введите lastName">
            </div>
            <div class="form-group">
                <label>Department</label>
                <select class="form-control" v-model="item.profile.idDepartment">
                    <option v-bind:value="item.id" v-for="item in state.departments">{{item.title}}</option>
                </select>
            </div>
            <!-- error message -->
            <div class="text-danger" v-for="(error, index) in profileErrors" :key="index">
                <div class="error-msg">{{ error[0] }}</div>
            </div>
            <div class="row m-0 justify-content-end ">
                <div class="p-3">
                    <button class="btn btn-primary" v-on:click="close">Отменить</button>
                </div>
                <div class="p-3">
                    <button type="submit" class="btn btn-success">Сохранить</button>
                </div>
            </div>
        </form>
    </div>
    
</template>
<script>
    import axios from 'axios'
    import { inject } from 'vue'

    export default {
        name: "editcomponent",
        props: ['item'],
        setup() {
            const state = inject('state')

            return {
                state
            }
        },
        data() {
            return {
                accountErrors: null,
                profileErrors: null
            }
        },
        computed: {

        },
        methods: {
            close() {
                // destroy the vue listeners, etc
                this.$emit('close-component')
            },
            // Сохранить Account
            saveAccount() {
                axios.put
                    ('https://blog.learn-courses.ru/gateway/account', this.item)
                    .then(response => {
                        // Передаем в компонент родителя действие и итем, чтобы уведомить о том, что успешное обновление данных
                        this.$emit('update-account', this.item)

                    }).catch(error => {
                        this.accountErrors = error.response.data.errors

                        console.log(error.response.data.errors)
                    });
            },
            saveProfile() {
                axios.put
                    ('https://blog.learn-courses.ru/gateway/profile', this.item.profile)
                    .then(response => {
                        // Передаем в компонент родителя действие и итем, чтобы уведомить о том, что успешное обновление данных
                        this.$emit('update-account', this.item)

                    }).catch(error => {
                        this.profileErrors = error.response.data.errors

                        console.log(error.response.data.errors)
                    });
            }
        },
        created() {

        },
        beforeMount() {

        },
        mounted() {
            
        },
    }
</script>
