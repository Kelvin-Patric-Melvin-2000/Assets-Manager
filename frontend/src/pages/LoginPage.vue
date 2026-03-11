<template><div class="card"><h2>Login</h2><div class="small">No account? <router-link to="/register">Register</router-link></div><label>Email<input v-model="form.email" /></label><label>Password<input type="password" v-model="form.password" /></label><button @click="submit">Login</button><p>{{ error }}</p></div></template>
<script setup>
import { reactive, ref } from 'vue'; import { useRouter } from 'vue-router'; import api from '../services/api';
const router = useRouter(); const error = ref(''); const form = reactive({ email: '', password: '' });
const submit = async () => { try { const { data } = await api.post('/auth/login', form); localStorage.setItem('token', data.token); router.push('/') } catch (e) { error.value = e.response?.data?.message || 'Login failed' } }
</script>
