<template>
  <div class="max-w-4xl mx-auto px-4 py-8">
    <div class="bg-white rounded-lg shadow-lg p-6">
      <div class="mb-6 pb-4 border-b border-gray-200">
        <h1 class="text-2xl font-bold text-gray-900">
          Nueva Venta de Criptomonedas
        </h1>
      </div>
      <TransactionForm
        action="sale"
        submit-text="Registrar Venta"
        @submit="(payload, done) => handleSubmit(payload, done)"
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

const handleSubmit = async (data, done) => {
  try {
    await transactionStore.create(data)
    done(null)
    router.push('/transactions/history')
  } catch (err) {
    done(err.response?.data || 'Error al crear transacción')
  }
}
</script>
