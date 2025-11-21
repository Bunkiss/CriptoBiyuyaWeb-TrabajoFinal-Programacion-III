<template>
  <div class="max-w-4xl mx-auto px-4 py-8">
    <RouterLink to="/transactions/history" class="inline-flex items-center gap-2 text-blue-600 hover:text-blue-700 mb-6 font-medium">
      ← Volver
    </RouterLink>

    <div class="bg-white rounded-lg shadow-lg overflow-hidden">
      <div class="px-6 py-4 bg-blue-50 border-b border-gray-200">
        <h1 class="text-2xl font-bold text-gray-900">
          Detalles de Transacción
        </h1>
      </div>
      <div class="p-6">
        <div v-if="isLoading" class="flex justify-center items-center py-12">
          <div class="text-center">
            <div class="text-4xl text-blue-600 mb-4">⏳</div>
            <p class="text-gray-600">Cargando...</p>
          </div>
        </div>

        <div v-else-if="error" class="p-4 bg-red-50 border border-red-200 text-red-700 rounded-lg">
          {{ error }}
        </div>

        <div v-else-if="transaction" class="space-y-6">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div class="border border-gray-200 rounded-lg p-4">
              <p class="text-gray-600 text-sm font-medium mb-1">ID</p>
              <p class="text-lg font-bold text-gray-900">{{ transaction.id }}</p>
            </div>

            <div class="border border-gray-200 rounded-lg p-4">
              <p class="text-gray-600 text-sm font-medium mb-1">Cliente</p>
              <p class="text-lg font-bold text-gray-900">{{ transaction.client?.name || 'N/A' }}</p>
            </div>

            <div class="border border-gray-200 rounded-lg p-4">
              <p class="text-gray-600 text-sm font-medium mb-1">Criptomoneda</p>
              <span class="inline-block bg-blue-100 text-blue-800 px-3 py-1 rounded font-medium">
                {{ formatCryptoName(transaction.crypto_code) }}
              </span>
            </div>

            <div class="border border-gray-200 rounded-lg p-4">
              <p class="text-gray-600 text-sm font-medium mb-1">Tipo</p>
              <span
                :class="[
                  'inline-block px-3 py-1 rounded font-medium',
                  transaction.action === 'purchase' 
                    ? 'bg-green-100 text-green-800' 
                    : 'bg-red-100 text-red-800',
                ]"
              >
                {{ formatAction(transaction.action) }}
              </span>
            </div>

            <div class="border border-gray-200 rounded-lg p-4">
              <p class="text-gray-600 text-sm font-medium mb-1">Cantidad de Cripto</p>
              <p class="text-lg font-bold text-gray-900">{{ formatCryptoAmount(transaction.crypto_amount) }}</p>
            </div>

            <div class="border border-gray-200 rounded-lg p-4">
              <p class="text-gray-600 text-sm font-medium mb-1">Monto (ARS)</p>
              <p class="text-lg font-bold text-blue-600">{{ formatMoney(transaction.money) }}</p>
            </div>

            <div class="border border-gray-200 rounded-lg p-4 md:col-span-2">
              <p class="text-gray-600 text-sm font-medium mb-1">Fecha y Hora</p>
              <p class="text-lg font-bold text-gray-900">{{ formatDateTime(transaction.datetime) }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, RouterLink } from 'vue-router'
import { useTransactionStore } from '../stores/transactionStore'
import { useFormatTransaction } from '../composables/useFormatTransaction'

const route = useRoute()
const transactionStore = useTransactionStore()
const { formatCryptoName, formatAction, formatMoney, formatDateTime, formatCryptoAmount } =
  useFormatTransaction()

const transaction = ref(null)
const isLoading = ref(true)
const error = ref(null)

onMounted(async () => {
  const id = Number(route.params.id)
  try {
    transaction.value = await transactionStore.getById(id)
  } catch (err) {
    error.value = err.response?.data?.message || 'Error al cargar la transacción'
  } finally {
    isLoading.value = false
  }
})
</script>
