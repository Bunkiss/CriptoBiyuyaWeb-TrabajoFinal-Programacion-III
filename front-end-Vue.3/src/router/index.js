import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/modules/home/views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/transaction',
      name: 'Transaction',
      component: () => import('@/modules/transaction/views/TransactionView.vue'),
    },
    {
      path: '/historial',
      name: 'Clients',
      component: () => import('@/modules/clients/views/ClientsView.vue'),
    },
  ],
})

export default router
