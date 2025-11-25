<template>
  <form @submit.prevent="handleFormSubmit" class="space-y-6">
    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
      <div>
        <label for="client" class="block text-sm font-medium text-gray-700 mb-1">
          Cliente <span class="text-red-500">*</span>
        </label>
        <select
          id="client"
          v-model="clientId"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 cursor-pointer"
          :class="{ 'border-red-500': errors.clientId }"
          :disabled="isLoading"
        >
          <option value="">Seleccionar cliente...</option>
          <option v-for="client in clients" :key="client.id" :value="client.id">
            {{ client.name }}
          </option>
        </select>
        <div v-if="errors.clientId" class="text-red-600 text-sm mt-1">
          {{ errors.clientId }}
        </div>
      </div>

      <div>
        <label for="crypto" class="block text-sm font-medium text-gray-700 mb-1">
          Criptomoneda <span class="text-red-500">*</span>
        </label>
        <select
          id="crypto"
          v-model="cryptoCode"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 cursor-pointer"
          :class="{ 'border-red-500': errors.cryptoCode }"
          :disabled="isLoading"
        >
          <option value="">Seleccionar criptomoneda...</option>
          <option v-for="crypto in CRYPTO_OPTIONS" :key="crypto.code" :value="crypto.code">
            {{ crypto.label }}
          </option>
        </select>
        <div v-if="errors.cryptoCode" class="text-red-600 text-sm mt-1">
          {{ errors.cryptoCode }}
        </div>
      </div>
    </div>

    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
      <div>
        <label for="amount" class="block text-sm font-medium text-gray-700 mb-1">
          Cantidad de Cripto <span class="text-red-500">*</span>
        </label>
        <input
          id="amount"
          v-model="cryptoAmount"
          type="number"
          step="0.00000001"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
          :class="{ 'border-red-500': errors.cryptoAmount }"
          placeholder="0.00000000"
          :disabled="isLoading"
        />
        <div v-if="errors.cryptoAmount" class="text-red-600 text-sm mt-1">
          {{ errors.cryptoAmount }}
        </div>
      </div>

      <div>
        <label for="datetime" class="block text-sm font-medium text-gray-700 mb-1">
          Fecha y Hora <span class="text-red-500">*</span>
        </label>
        <input
          id="datetime"
          v-model="datetime"
          type="datetime-local"
          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 cursor-pointer"
          :class="{ 'border-red-500': errors.datetime }"
          :disabled="isLoading"
        />
        <div v-if="errors.datetime" class="text-red-600 text-sm mt-1">
          {{ errors.datetime }}
        </div>
      </div>
    </div>

    <div v-if="apiError" class="p-3 bg-red-50 border border-red-200 text-red-700 rounded-lg text-sm">
      {{ apiError }}
    </div>

    <div class="flex gap-2 pt-4">
      <button
        type="submit"
        class="flex-1 bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-lg transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-2 cursor-pointer"
        :disabled="isLoading"
      >
        {{ isLoading ? '⏳ Procesando...' : submitText }}
      </button>
      <button 
        type="button" 
        class="bg-gray-600 hover:bg-gray-700 text-white font-medium py-2 px-4 rounded-lg transition-colors disabled:opacity-50 cursor-pointer"
        @click="$emit('cancel')" 
        :disabled="isLoading"
      >
        Cancelar
      </button>
    </div>
  </form>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useForm, useField } from 'vee-validate'
import { transactionSchema } from '../composables/schemas'
import { useClientStore } from '../stores/clientStore'

const CRYPTO_OPTIONS = [
  { code: 'btc', label: 'Bitcoin' },
  { code: 'eth', label: 'Ethereum' },
  { code: 'usdc', label: 'USD Coin' }
]

const props = defineProps({
  action: {
    type: String,
    required: true,
    validator: (value) => ['purchase', 'sale'].includes(value),
  },
  submitText: {
    type: String,
    default: 'Crear Transacción',
  },
})

const emit = defineEmits(['submit', 'cancel'])

const clientStore = useClientStore()
const clients = computed(() => clientStore.sortedClients)

onMounted(() => {
  clientStore.fetchAll()
})

const { handleSubmit, errors } = useForm({
  validationSchema: transactionSchema,
})

const { value: clientId } = useField('clientId')
const { value: cryptoCode } = useField('cryptoCode')
const { value: cryptoAmount } = useField('cryptoAmount')
const { value: datetime } = useField('datetime')

const isLoading = ref(false)
const apiError = ref(null)

const handleFormSubmit = handleSubmit(async (values) => {
  isLoading.value = true
  apiError.value = null

  try {
    const formattedDatetime = new Date(values.datetime).toISOString()
    const payload = {
      client_id: Number(values.clientId),
      crypto_code: values.cryptoCode,
      action: props.action,
      crypto_amount: Number(values.cryptoAmount),
      datetime: formattedDatetime,
    }

    emit('submit', payload, (errorMessage) => {
      if (errorMessage) {
        apiError.value = errorMessage
      }
      
      isLoading.value = false
    })
  } catch (err) {
    apiError.value = err.message || 'Error al procesar la transacción'
  }
})
</script>
