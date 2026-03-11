<template>
  <div class="card"><button @click="$router.push('/vehicles/new')">Add Vehicle</button></div>
  <div class="card"><table class="table"><thead><tr><th>Name</th><th>Brand</th><th>Mileage</th><th></th></tr></thead><tbody><tr v-for="v in vehicles" :key="v.id"><td>{{ v.name }}</td><td>{{ v.brand }} {{ v.model }}</td><td>{{ v.currentMileage }}</td><td><button @click="$router.push(`/vehicles/${v.id}`)">View</button><button class="secondary" @click="$router.push(`/vehicles/${v.id}/edit`)">Edit</button></td></tr></tbody></table></div>
</template>
<script setup>
import { onMounted, ref } from 'vue'; import api from '../services/api';
const vehicles = ref([]); onMounted(async () => { const { data } = await api.get('/vehicles'); vehicles.value = data.items })
</script>
