<template>
  <div class="max-w-4xl mx-auto px-4 py-8">
    <div v-if="isLoading" class="flex justify-center items-center py-12">
      <div class="text-center">
        <div class="text-4xl text-blue-600 mb-4">⏳</div>
        <p class="text-gray-600">Cargando...</p>
      </div>
    </div>

    <div v-else-if="error" class="bg-red-50 border border-red-200 text-red-700 rounded-lg p-4">
      {{ error }}
    </div>

    <div v-else-if="transaction" class="bg-white rounded-lg shadow-lg overflow-hidden">
      <div class="px-6 py-4 border-b border-gray-200">
        <h1 class="text-2xl font-bold text-gray-900">
          Editar Transacción
        </h1>
      </div>
      <div class="p-6">
        <TransactionEditForm
          :transaction="transaction"
          @submit="handleSubmit"
          @cancel="$router.back()"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useTransactionStore } from '../stores/transactionStore.js'
import TransactionEditForm from '../components/TransactionEditForm.vue'

const route = useRoute()
const router = useRouter()
const transactionStore = useTransactionStore()

const transaction = ref(null)
const isLoading = ref(true)
const error = ref(null)

onMounted(async () => {
  const id = Number(route.params.id)
  try {
    transaction.value = await transactionStore.getById(id)
  } catch (err) {
    error.value = 'Error al cargar la transacción'
  } finally {
    isLoading.value = false
  }
})

const handleSubmit = async (data) => {
  const id = Number(route.params.id)
  try {
    await transactionStore.update(id, data)
    router.push('/transactions/history')
  } catch (err) {
    error.value = 'Error al actualizar la transacción'
  }
}
</script>
