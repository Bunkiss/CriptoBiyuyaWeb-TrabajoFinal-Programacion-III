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
      path: '/nueva-compra',
      name: 'NuevaCompraView',
      component: () => import('@/modules/compra/views/RealizarCompraView.vue'),
    },
  ],
})

export default router
