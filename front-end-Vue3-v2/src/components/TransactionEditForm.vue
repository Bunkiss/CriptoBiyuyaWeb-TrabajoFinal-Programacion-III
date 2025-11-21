<template>
  <form @submit.prevent="handleFormSubmit" class="space-y-6">
    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
      <div>
        <label for="client" class="block text-sm font-medium text-gray-700 mb-1">
          Cliente
        </label>
        <select
          id="client"
          v-model="clientId"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          :disabled="isLoading"
        >
          <option value="">Seleccionar cliente...</option>
          <option v-for="client in clients" :key="client.id" :value="client.id">
            {{ client.name }}
          </option>
        </select>
      </div>

      <div>
        <label for="crypto" class="block text-sm font-medium text-gray-700 mb-1">
          Criptomoneda
        </label>
        <select
          id="crypto"
          v-model="cryptoCode"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          :disabled="isLoading"
        >
          <option value="">Seleccionar criptomoneda...</option>
          <option v-for="crypto in CRYPTO_OPTIONS" :key="crypto.code" :value="crypto.code">
            {{ crypto.label }}
          </option>
        </select>
      </div>
    </div>

    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
      <div>
        <label for="amount" class="block text-sm font-medium text-gray-700 mb-1">
          Cantidad
        </label>
        <input
          id="amount"
          v-model="cryptoAmount"
          type="number"
          step="0.00000001"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="0.00000000"
          :disabled="isLoading"
        />
      </div>

      <div>
        <label for="money" class="block text-sm font-medium text-gray-700 mb-1">
          Monto (ARS)
        </label>
        <input
          id="money"
          v-model="money"
          type="number"
          step="0.01"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="0.00"
          :disabled="isLoading"
        />
      </div>
    </div>

    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
      <div>
        <label for="datetime" class="block text-sm font-medium text-gray-700 mb-1">
          Fecha y Hora
        </label>
        <input
          id="datetime"
          v-model="datetime"
          type="datetime-local"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          :disabled="isLoading"
        />
      </div>

      <div>
        <label for="action" class="block text-sm font-medium text-gray-700 mb-1">
          Tipo
        </label>
        <select
          id="action"
          v-model="action"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          :disabled="isLoading"
        >
          <option value="purchase">Compra</option>
          <option value="sale">Venta</option>
        </select>
      </div>
    </div>

    <div v-if="apiError" class="p-3 bg-red-50 border border-red-200 text-red-700 rounded-lg text-sm">
      {{ apiError }}
    </div>

    <div class="flex gap-2 pt-4">
      <button
        type="submit"
        class="flex-1 bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-2"
        :disabled="isLoading"
      >
        {{ isLoading ? '⏳ Guardando...' : 'Guardar Cambios' }}
      </button>
      <button 
        type="button" 
        class="bg-gray-600 hover:bg-gray-700 text-white font-medium py-2 px-4 rounded-lg transition-colors disabled:opacity-50"
        @click="$emit('cancel')" 
        :disabled="isLoading"
      >
        Cancelar
      </button>
    </div>
  </form>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useClientStore } from '../stores/clientStore'

const CRYPTO_OPTIONS = [
  { code: 'btc', label: 'Bitcoin' },
  { code: 'eth', label: 'Ethereum' },
  { code: 'usdc', label: 'USD Coin' }
]

const props = defineProps({
  transaction: {
    type: Object,
    required: true,
  },
})

const emit = defineEmits(['submit', 'cancel'])

const clientStore = useClientStore()
const clients = computed(() => clientStore.sortedClients)

const clientId = ref(props.transaction.client_id)
const cryptoCode = ref(props.transaction.crypto_code)
const cryptoAmount = ref(props.transaction.crypto_amount)
const money = ref(props.transaction.money)
const datetime = ref(new Date(props.transaction.datetime).toISOString().slice(0, 16))
const action = ref(props.transaction.action)

const isLoading = ref(false)
const apiError = ref(null)

const handleFormSubmit = async () => {
  isLoading.value = true
  apiError.value = null

  try {
    const formattedDatetime = new Date(datetime.value).toISOString()
    const payload = {
      client_id: Number(clientId.value),
      crypto_code: cryptoCode.value,
      action: action.value,
      crypto_amount: Number(cryptoAmount.value),
      money: Number(money.value),
      datetime: formattedDatetime,
    }

    emit('submit', payload)
  } catch (err) {
    apiError.value = err.message || 'Error al procesar la transacción'
  } finally {
    isLoading.value = false
  }
}
</script>
