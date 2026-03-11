<template>
  <nav>
    <router-link to="/">Dashboard</router-link>
    <router-link to="/vehicles">Vehicles</router-link>
    <router-link to="/login" v-if="!token">Login</router-link>
    <a href="#" v-if="token" @click.prevent="logout">Logout</a>
  </nav>
  <div class="container"><router-view /></div>
</template>
<script setup>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
const router = useRouter()
const token = computed(() => localStorage.getItem('token'))
const logout = () => { localStorage.clear(); router.push('/login') }
</script>
