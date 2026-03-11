<template>
  <div class="card">
    <h2>Vehicle Details</h2>
    <div class="grid grid-2">
      <label>Latest Mileage<input type="number" v-model.number="mileage" /></label>
      <button @click="addMileage">Update Mileage</button>
    </div>
  </div>
  <div class="grid grid-2">
    <div class="card"><h3>Fuel Log</h3><label>Litres<input type="number" v-model.number="fuel.fuelAmountLitres" /></label><label>Cost<input type="number" v-model.number="fuel.fuelCost" /></label><label>Mileage At Fill<input type="number" v-model.number="fuel.mileageAtFill" /></label><button @click="addFuel">Add Fuel</button><ul><li v-for="f in fuelLogs" :key="f.id">{{ f.date }} - ₹{{ f.fuelCost }}</li></ul></div>
    <div class="card"><h3>Services</h3><label>Name<input v-model="service.serviceName" /></label><label>Last Date<input type="date" v-model="service.lastServiceDate" /></label><label>Last Mileage<input type="number" v-model.number="service.lastServiceMileage" /></label><label>Interval Months<input type="number" v-model.number="service.serviceIntervalMonths" /></label><label>Interval KM<input type="number" v-model.number="service.serviceIntervalKm" /></label><button @click="addService">Add Service</button></div>
    <div class="card"><h3>Documents</h3><label>Type<select v-model.number="document.documentType"><option :value="1">RC</option><option :value="2">Insurance</option><option :value="3">PUCC</option></select></label><label>File Path<input v-model="document.filePath" /></label><label>Issue Date<input type="date" v-model="document.issueDate" /></label><label>Expiry Date<input type="date" v-model="document.expiryDate" /></label><button @click="addDocument">Add Document</button></div>
    <div class="card"><h3>Parts</h3><label>Part Name<input v-model="part.partName" /></label><label>Date<input type="date" v-model="part.replacementDate" /></label><label>Mileage<input type="number" v-model.number="part.mileageAtReplacement" /></label><button @click="addPart">Add Part</button></div>
  </div>
</template>
<script setup>
import { onMounted, reactive, ref } from 'vue'; import { useRoute } from 'vue-router'; import api from '../services/api';
const route = useRoute(); const vehicleId = route.params.id; const mileage = ref(0); const fuelLogs = ref([])
const fuel = reactive({ vehicleId, fuelAmountLitres:0, fuelCost:0, mileageAtFill:0 })
const service = reactive({ vehicleId, serviceName:'General Service', lastServiceDate:new Date().toISOString().slice(0,10), lastServiceMileage:0, serviceIntervalMonths:6, serviceIntervalKm:10000 })
const document = reactive({ vehicleId, documentType:1, filePath:'', issueDate:new Date().toISOString().slice(0,10), expiryDate:new Date().toISOString().slice(0,10) })
const part = reactive({ vehicleId, partName:'Engine Oil', replacementDate:new Date().toISOString().slice(0,10), mileageAtReplacement:0, notes:'' })
const load = async()=>{ fuelLogs.value = (await api.get(`/fuel/${vehicleId}`)).data }
onMounted(load)
const addMileage = async()=>{ await api.post('/mileage',{ vehicleId, mileage }); }
const addFuel = async()=>{ await api.post('/fuel', fuel); await load() }
const addService = async()=>{ await api.post('/services', service) }
const addDocument = async()=>{ await api.post('/documents', document) }
const addPart = async()=>{ await api.post('/parts', part) }
</script>
