<template>
    <div class="row">
        <div class="col-md-12">
            <h1>Engines</h1>
            
            <div class="my-2">
                <router-link to="/roster/engines/new" class="btn btn-outline-success">New Engine</router-link>
            </div>

            <table class="table table-condensed table-striped table-hover">
                <tr>
                    <th>Road</th>
                    <th>Number</th>
                    <th>Type</th>
                    <th>Model</th>
                    <th>Length</th>
                    <th></th>
                </tr>
                <tbody>
                    <tr v-for="engine in engines" v-bind:key="engine.id">
                        <td>{{ engine.road.name }}</td>
                        <td>{{ engine.roadNumber }}</td>
                        <td>{{ engine.type.name }}</td>
                        <td>{{ engine.model.name }}</td>
                        <td>{{ engine.length }}</td>
                        <td>
                            <router-link :to="{name: 'EditEngine', params: { id: engine.id }}">Edit</router-link>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'EngineList',
        data () {
            return { 
                engines: null
            }
        },
        created: function() {
            this.getEngines();
        },
        methods: { 
            getEngines: function() {
                var self = this;

                this.$http.get('engines').then(function(response) {
                    self.engines = response.data
                }, function(err) {
                    console.log(err);
                })
            }
        }
    }
</script>
