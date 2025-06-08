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
      name: 'TransactionView',
      component: () => import('@/modules/transaction/views/TransactionView.vue'),
    },
    {
      path: '/historial',
      name: 'ClientsView',
      component: () => import('@/modules/client/views/ClientsView.vue'),
    },
  ],
})

export default router
