<template>
  <div class="max-w-4xl mx-auto px-4 py-8">
    <div class="bg-white rounded-lg shadow-lg p-6">
      <div class="mb-6 pb-4 border-b border-gray-200">
        <h1 class="text-2xl font-bold text-gray-900">
          Nueva Compra de Criptomonedas
        </h1>
      </div>
      <TransactionForm
        action="purchase"
        submit-text="Registrar Compra"
        @submit="handleSubmit"
        @cancel="$router.back()"
      />
    </div>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import TransactionForm from '../components/TransactionForm.vue'
import { useTransactionStore } from '../stores/transactionStore.js'

const router = useRouter()
const transactionStore = useTransactionStore()

const handleSubmit = async (data) => {
  try {
    await transactionStore.create(data)
    router.push('/transactions/history')
  } catch (err) {
    return err.response?.data?.message || 'Error al crear transacción'
  }
}
</script>
