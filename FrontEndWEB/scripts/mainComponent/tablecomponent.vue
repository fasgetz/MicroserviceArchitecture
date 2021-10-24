<template>
    <div>
        <div>
            <div class="overflow-auto custom-table mt-3">
                <div class="row m-3 justify-content-between">
                    <div class="col-12 col-lg-6">
                        <div class="d-flex">
                            <button v-on:click="addAccount" class="btn btn-primary m-3">+</button>
                            <input class="form-control m-3" placeholder="Значение для фильтрации" type="text" v-model="filterText" />
                        </div>
                    </div>
                    <div class="col-12 col-lg-6">
                        <div class="d-flex align-items-center">
                            <div class="align-items-center m-3">Department</div>
                            <select class="form-control m-3" v-model="selectedDepartment">
                                <option value="0">Весь список</option>
                                <option v-bind:value="item.id" v-for="item in departments">{{item.title}}</option>
                            </select>
                        </div>
                    </div>
                </div>
                <table class="table">
                    <thead>
                        <tr>
                            <th scope="col">Name</th>
                            <th scope="col">Email</th>
                            <th scope="col">FirstName</th>
                            <th scope="col">LastName</th>
                            <th scope="col">Department</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody v-for="item in filteredList" v-bind:key="item">
                        <tr>
                            <td>{{item.name}}</td>
                            <td>{{item.email}}</td>
                            <td>{{item.profile.firstName}}</td>
                            <td>{{item.profile.lastName}}</td>
                            <td>{{item.profile.department != null ? item.profile.department.title : null}}</td>
                            <td>
                                <div class="d-flex">
                                    <div class="p-1">
                                        <button class="btn btn-primary" v-on:click="editItem(item)">EDIT</button>
                                    </div>
                                    <div class="p-1">
                                        <button class="btn btn-danger" v-on:click="deleteItem(item)">DEL</button>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <component :is="currentTabComponent" v-bind="currentProperties" v-on:added-account="addedAccount" v-on:update-account="updateAccount" v-on:close-component="deleteThisRow"></component>
        
    </div>
</template>
<script>
    import { reactive, provide } from 'vue';
    import axios from 'axios'
    import addcomponent from './addcomponent.vue'
    import editcomponent from './editcomponent.vue'

    const apiUrl = "http://192.168.1.66:6000/"

    export default {
        name: "tablecomponent",
        components: {
            addcomponent,
            editcomponent
        },
        setup() {
            const state = reactive({ departments: null });


            provide('state', state)

            return { state };
        },
        data() {
            return {
                selectedDepartment: 0,
                selectedUser: null,
                filterText: '',
                users: null,
                departments: null, // departments
                currentTab: '',
                tabs: ['addcomponent', 'editcomponent']
            }
        },
        computed: {
            // Фильтрация
            filteredList() {
                if (this.filterText || this.selectedDepartment) {

                    // Фильтр
                    const filter = event =>
                        event.name.toLowerCase().includes(this.filterText) ||
                        event.email.toLowerCase().includes(this.filterText)

                    let result = this.users.filter(filter)

                    // Фильтр select (айди департамента)
                    if (this.selectedDepartment != 0) {
                        result = result.filter(event => event.profile.idDepartment == this.selectedDepartment)
                    }

                    return result
                } else {
                    return this.users;
                }
            },
            currentTabComponent() {
                return this.currentTab.toLowerCase()
            },
            currentProperties: function () {

                if (this.currentTab === 'editcomponent') {

                    return { item: this.selectedUser }
                }
                else {
                    console.log('elsed')
                }
            }
        },
        methods: {
            // Добавление данных в таблицу без доп запроса
            addedAccount: function (item) {
                console.log(item)

                this.users.push(item)

                this.deleteThisRow()
            },
            deleteItem: function (item) {
                this.deleteThisRow()

                axios.delete
                    ('https://blog.learn-courses.ru/gateway/account', {
                        params:
                        {
                            id: item.id
                        }
                    })
                    .then(response => {
                        // Обновить данные в таблице путем удаления итема по индексу, чтобы не дергать из базы юзеров
                        let index = this.users.map(e => e.id).indexOf(item.id);

                        this.users.splice(index, 1);

                    }).catch(error => {

                        console.log(error.response.data.errors)
                    });
            },
            // emit - обновление данных в таблице без доп запроса
            updateAccount: function (item) {
                // Обновляем в масиве данных, чтобы не делать запрос к БД на обновление
                let index = this.users.map(e => e.id).indexOf(item.id);

                // Если обновляем профиль, то необходимо изменить title профиля
                let indexDepartUpdate = this.departments.map(e => e.id).indexOf(item.profile.idDepartment) // Индекс департмента для обновления тайтла

                console.log('indexDepartUpdate: ', indexDepartUpdate)


                // Если профиль ранее был пустой, то добавить ему тайтл вручную
                if (item.profile.idDepartment != null) {
                    console.log('is NOT null:', item.profile)

                    item.profile.department = {
                        title: this.departments[indexDepartUpdate].title
                    }
                }
                // Иначе, если профиль не был пустым, то обновить ему тайтл
                else {
                    console.log('is null:', item.profile)

                        
                }

                


                this.users[index] = item
                this.deleteThisRow()
            },
            addAccount: function () {
                this.currentTab = 'addcomponent'
            },
            editItem: function (item) {
                this.selectedUser = JSON.parse(JSON.stringify(item))

                this.currentTab = 'editcomponent'

                console.log(item)
            },
            deleteThisRow: function () {
                this.currentTab = ''
            },


        },
        beforeMount() {
            axios.get('https://blog.learn-courses.ru/gateway/department')
                .then(response => {
                    this.departments = response.data

                    this.state.departments = response.data
                }).catch(error => {
                    alert('error id')
                });

            axios.get
                ('https://blog.learn-courses.ru/gateway/users')
                .then(response => {
                    this.users = response.data
                }).catch(error => {
                    alert('error id')
                });
        },
        mounted() {

        },
    }
</script>
