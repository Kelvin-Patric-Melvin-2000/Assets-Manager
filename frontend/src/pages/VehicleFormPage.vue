<template><div class="card"><h2>{{ editMode ? 'Edit' : 'Add' }} Vehicle</h2><div class="grid grid-2"><label>Name<input v-model="form.name" /></label><label>Brand<input v-model="form.brand" /></label><label>Model<input v-model="form.model" /></label><label>Registration<input v-model="form.registrationNumber" /></label><label>Type<input v-model="form.vehicleType" /></label><label>Purchase Date<input type="date" v-model="form.purchaseDate" /></label><label>Current Mileage<input type="number" v-model.number="form.currentMileage" /></label></div><button @click="submit">Save</button></div></template>
<script setup>
import { computed, onMounted, reactive } from 'vue'; import { useRoute, useRouter } from 'vue-router'; import api from '../services/api';
const route = useRoute(); const router = useRouter(); const editMode = computed(() => !!route.params.id)
const form = reactive({ name:'', brand:'', model:'', registrationNumber:'', vehicleType:'Bike', purchaseDate:new Date().toISOString().slice(0,10), currentMileage:0 })
onMounted(async () => { if (editMode.value) { const { data } = await api.get('/vehicles'); Object.assign(form, data.items.find(v => v.id === route.params.id)) } })
const submit = async () => { editMode.value ? await api.put(`/vehicles/${route.params.id}`, form) : await api.post('/vehicles', form); router.push('/vehicles') }
</script>
