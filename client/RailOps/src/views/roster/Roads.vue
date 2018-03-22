<template>
    <div class="row">
        <div class="col-md-12">
            <h1>Roads</h1>
            <div class="my-2">
                <div class="row">
                    <div class="col-md-4">
                        <form @submit.prevent="createRoad">
                            <div class="form-group">
                                <label for="new-road-name">Road name</label>
                                <input type="text" id="new-road-name" v-model="newRoad.name" class="form-control" />
                            </div>

                            <button type="submit" id="btn-new-road-save" class="btn btn-primary">Create</button>
                        </form>
                    </div>
                </div>
            </div>
            <table>
                <tr>
                    <th>Road name</th>
                    <th></th>
                </tr>
                <tr v-for="road in roads" v-bind:key="road.id">
                    <td>{{ road.name }}</td>
                    <td>
                        <button type="button" class="btn btn-link" @click="deleteRoad(road)">
                            <span class="text-danger">Delete</span>
                        </button>
                    </td>
                </tr>

            </table>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Roads',
        data () {
            return {
                roads: null,
                newRoad: {
                    name: null
                }
            }
        },
        created: function() {
            this.getRoads();
        },
        methods: {
            getRoads: function() {
                var self = this;

                this.$http.get('roads').then(function(response) {
                    self.roads = response.data;
                }, function(err) {
                    console.log(err);
                }, function(err) {
                    console.log(err);
                })
            },
            createRoad: function() {
                var self = this;
                
                this.$http.post('roads', self.newRoad).then(function() {
                    self.getRoads();
                    self.newRoad.name = null;
                }, function(err) {
                    console.log(err);
                });
            },
            deleteRoad: function(road) {
                var self = this;

                if (!confirm('Are you sure you want to delete ' + road.name + '?')) {
                    return;
                }

                this.$http.delete('roads/' + road.id).then(function() {
                    self.getRoads();
                }, function(err) {
                    console.log(err);
                });
            }
        }
    }
</script>
