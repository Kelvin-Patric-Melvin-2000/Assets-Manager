<template><div class="card"><h2>Register</h2><label>Name<input v-model="form.name" /></label><label>Email<input v-model="form.email" /></label><label>Password<input type="password" v-model="form.password" /></label><button @click="submit">Register</button><p>{{ message }}</p></div></template>
<script setup>
import { reactive, ref } from 'vue'; import { useRouter } from 'vue-router'; import api from '../services/api';
const router = useRouter(); const message = ref(''); const form = reactive({ name: '', email: '', password: '' });
const submit = async () => { await api.post('/auth/register', form); message.value = 'Registration successful'; setTimeout(() => router.push('/login'), 700) }
</script>
