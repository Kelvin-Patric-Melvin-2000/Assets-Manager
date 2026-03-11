import { createRouter, createWebHistory } from 'vue-router'
import LoginPage from '../pages/LoginPage.vue'
import RegisterPage from '../pages/RegisterPage.vue'
import DashboardPage from '../pages/DashboardPage.vue'
import VehicleListPage from '../pages/VehicleListPage.vue'
import VehicleFormPage from '../pages/VehicleFormPage.vue'
import VehicleDetailsPage from '../pages/VehicleDetailsPage.vue'

const routes = [
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage },
  { path: '/', component: DashboardPage },
  { path: '/vehicles', component: VehicleListPage },
  { path: '/vehicles/new', component: VehicleFormPage },
  { path: '/vehicles/:id/edit', component: VehicleFormPage },
  { path: '/vehicles/:id', component: VehicleDetailsPage }
]

const router = createRouter({ history: createWebHistory(), routes })
router.beforeEach((to) => {
  const publicRoutes = ['/login', '/register']
  if (!publicRoutes.includes(to.path) && !localStorage.getItem('token')) return '/login'
})
export default router
