<!-- TODO: Make the road/model/type dropdowns into a shared component -->

<template>
    <div class="row">
        <div class="col-md-12">
            <h1>Edit Engine</h1>

            <form @submit.prevent="saveEngine">
                <div class="row">
                    <div class="col-md-3 form-group">
                        <label for="road-id">Road</label>
                        <select v-model="engine.roadId" class="form-control">
                            <option id="road-id" v-for="road in roadOptions" v-bind:value="road.id" v-bind:key="road.id">
                                {{ road.name }}
                            </option>
                        </select>
                    </div>
                    <div class="col-md-3 form-group">
                        <label for="road-number">Number</label>
                        <input type="text" class="form-control" id="road-number" v-model="engine.roadNumber" />
                    </div>
                    <div class="col-md-3 form-group">
                        <label for="type-id">Engine type</label>
                        <select v-model="engine.typeId" class="form-control">
                            <option id="type-id" v-for="et in typeOptions" v-bind:value="et.id" v-bind:key="et.id">
                                {{ et.name }}
                            </option>
                        </select>
                    </div>
                    <div class="col-md-3 form-group">
                        <label for="model-id">Model</label>
                        <select v-model="engine.modelId" class="form-control">
                            <option id="model-id" v-for="model in modelOptions" v-bind:value="model.id" v-bind:key="model.id">
                                {{ model.name }}
                            </option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 form-group">
                        <label for="length">Length (scale ft)</label>
                        <input type="text" class="form-control" id="length" v-model="engine.length" />
                    </div>
                    <div class="col-md-3 form-group">
                        <label for="weight-tons">Weight (tons)</label>
                        <input type="text" class="form-control" id="weight=tons" v-model="engine.weightTons" />
                    </div>
                </div>

                <button type="submit" class="btn btn-primary">Save engine</button>
            </form>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'EditEngine',
        data () {
            return {
                engine: {
                    modelId: 0,
                    typeId: 0,
                    weightTons: 0,
                    roadId: 0,
                    roadNumber: null,
                    length: 0,
                    comments: null
                },
                roadOptions: null,
                typeOptions: null,
                modelOptions: null
            }
        },
        created: function() {
            this.getRoads();
            this.getEngineTypes();
            this.getEngineModels();

            this.loadEngine();
        },
        watch: {
            '$route': 'loadEngine'
        },
        methods: {
            loadEngine: function() {
                var self = this;
                var id = this.$route.params.id;

                this.$http.get('engines/' + id).then(function(response) {
                    self.engine = response.data;
                });
            },
            getRoads: function() {
                var self = this;

                this.$http.get('roads').then(function(response) {
                    self.roadOptions = response.data;
                })
            },
            getEngineTypes: function() {
                var self = this;

                this.$http.get('engineTypes').then(function(response) {
                    self.typeOptions = response.data;
                });
            },
            getEngineModels: function() {
                var self = this;

                this.$http.get('engineModels').then(function(response) {
                    self.modelOptions = response.data;
                });
            },
            saveEngine: function() {
                var self = this;

                this.$http.put('engines/' + self.engine.id, self.engine).then(function(response) {
                    self.$router.push('/roster/engines');
                }, function(err) {
                    console.log(err);
                })
            }
        }
    }
</script>
