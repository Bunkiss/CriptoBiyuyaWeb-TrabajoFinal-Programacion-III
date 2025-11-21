import { createRouter, createWebHistory } from 'vue-router'

const ClientCreateView = () => import('../views/ClientCreateView.vue')
const TransactionCreateView = () => import('../views/TransactionCreateView.vue')
const TransactionSaleView = () => import('../views/TransactionSaleView.vue')
const TransactionHistoryView = () => import('../views/TransactionHistoryView.vue')
const TransactionDetailView = () => import('../views/TransactionDetailView.vue')
const TransactionEditView = () => import('../views/TransactionEditView.vue')

const routes = [
  {
    path: '/',
    redirect: '/transactions/history',
  },
  {
    path: '/clients/create',
    name: 'ClientCreate',
    component: ClientCreateView,
  },
  {
    path: '/transactions/purchase',
    name: 'TransactionCreate',
    component: TransactionCreateView,
  },
  {
    path: '/transactions/sale',
    name: 'TransactionSale',
    component: TransactionSaleView,
  },
  {
    path: '/transactions/history',
    name: 'TransactionHistory',
    component: TransactionHistoryView,
  },
  {
    path: '/transactions/:id/view',
    name: 'TransactionDetail',
    component: TransactionDetailView,
  },
  {
    path: '/transactions/:id/edit',
    name: 'TransactionEdit',
    component: TransactionEditView,
  },
]

const router = createRouter({
  history: createWebHistory('/'),
  routes,
})

export default router
