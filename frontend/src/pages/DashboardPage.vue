<template>
  <div class="grid grid-2">
    <div class="card"><h3>Vehicles</h3><h1>{{ dashboard.vehicleCount || 0 }}</h1></div>
    <div class="card"><h3>Fuel Economy (Latest/Avg/Best)</h3><p>{{ dashboard.fuelAnalytics?.latestFuelEconomy?.toFixed?.(2) || 0 }} / {{ dashboard.fuelAnalytics?.averageFuelEconomy?.toFixed?.(2) || 0 }} / {{ dashboard.fuelAnalytics?.bestFuelEconomy?.toFixed?.(2) || 0 }} km/L</p></div>
  </div>
  <div class="card"><h3>Upcoming Services</h3><ul><li v-for="s in dashboard.upcomingServices || []" :key="s.id">{{ s.serviceName }} - {{ s.nextServiceDate }}</li></ul></div>
  <div class="card"><h3>Expiring Documents</h3><ul><li v-for="d in dashboard.expiringDocuments || []" :key="d.id">{{ d.documentType }} expires on {{ d.expiryDate }}</li></ul></div>
</template>
<script setup>
import { onMounted, ref } from 'vue'; import api from '../services/api';
const dashboard = ref({}); onMounted(async () => { const { data } = await api.get('/dashboard'); dashboard.value = data })
</script>
