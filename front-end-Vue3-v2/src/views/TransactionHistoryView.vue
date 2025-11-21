<template>
  <div class="max-w-7xl mx-auto px-4 py-8">
    <div class="mb-8">
      <h1 class="text-3xl font-bold text-gray-900 mb-6">
        📋 Historial de Transacciones
      </h1>
    </div>

    <div class="mb-6 grid grid-cols-1 md:grid-cols-2 gap-4">
      <div class="flex gap-2">
        <select 
          v-model="selectedClientId" 
          class="flex-1 px-3 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          <option value="">-- Seleccionar cliente --</option>
          <option v-for="client in clients" :key="client.id" :value="client.id">
            {{ client.name }}
          </option>
        </select>
        <button
          type="button"
          class="bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-6 rounded-lg transition-colors disabled:opacity-50 flex items-center gap-2 cursor-pointer"
          @click="loadTransactionsByClient"
          :disabled="!selectedClientId || isLoading"
        >
          {{ isLoading ? '⏳' : '🔍' }} Cargar
        </button>
      </div>
      <div class="flex gap-2 md:justify-end">
        <RouterLink to="/transactions/purchase" class="bg-green-600 hover:bg-green-700 text-white font-medium py-2 px-4 rounded-lg transition-colors flex items-center gap-2">
          Nueva Compra
        </RouterLink>
        <RouterLink to="/transactions/sale" class="bg-yellow-600 hover:bg-yellow-700 text-white font-medium py-2 px-4 rounded-lg transition-colors flex items-center gap-2">
          Nueva Venta
        </RouterLink>
      </div>
    </div>

    <div v-if="error" class="mb-6 p-4 bg-red-50 border border-red-200 text-red-700 rounded-lg flex items-start gap-2">
      <div class="flex-1">⚠️ {{ error }}</div>
      <button @click="error = null" class="text-red-500 hover:text-red-700">
        ✕
      </button>
    </div>

    <!-- Delete Modal -->
    <div 
      v-if="showDeleteModal" 
      class="fixed inset-0 bg-gray-900 bg-opacity-40 flex items-center justify-center z-50 p-4"
    >
      <div class="bg-white rounded-2xl shadow-xl max-w-sm w-full overflow-hidden">
        
        <div class="px-6 py-4 border-b border-gray-200 bg-red-50 rounded-t-2xl">
          <h3 class="text-lg font-bold text-red-700">
            ⚠️ Confirmar eliminación
          </h3>
        </div>

        <div class="px-6 py-4">
          <p class="text-gray-700">¿Está seguro que desea eliminar esta transacción?</p>
          <p class="text-gray-400 text-sm mt-2">Esta acción no se puede deshacer.</p>
        </div>

        <div class="px-6 py-4 border-t border-gray-200 flex gap-2 justify-end rounded-b-2xl">
          <button
            type="button"
            class="bg-gray-600 hover:bg-gray-700 text-white font-medium py-2 px-4 rounded-lg transition-colors"
            @click="showDeleteModal = false"
          >
            Cancelar
          </button>

          <button
            type="button"
            class="bg-red-600 hover:bg-red-700 text-white font-medium py-2 px-4 rounded-lg transition-colors disabled:opacity-50 flex items-center gap-2"
            @click="confirmDelete"
            :disabled="isDeleting"
          >
            {{ isDeleting ? '⏳ Eliminando...' : 'Eliminar' }}
          </button>
        </div>
      </div>
    </div>

    <div class="bg-white rounded-lg shadow-lg overflow-hidden">
      <div class="px-6 py-4 bg-gray-50 border-b border-gray-200">
        <h2 class="text-lg font-bold text-gray-900">{{ transactions.length }} Transacciones</h2>
      </div>
      <div class="p-6">
        <TransactionTable
          :transactions="transactions"
          @view="viewTransaction"
          @edit="editTransaction"
          @delete="deleteTransaction"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter, RouterLink } from 'vue-router'
import { useClientStore } from '../stores/clientStore'
import { useTransactionStore } from '../stores/transactionStore'
import TransactionTable from '../components/TransactionTable.vue'

const router = useRouter()
const clientStore = useClientStore()
const transactionStore = useTransactionStore()

const clients = computed(() => clientStore.sortedClients)
const transactions = computed(() => transactionStore.sortedByDate)
const isLoading = computed(() => transactionStore.loading)
const isDeleting = ref(false)
const error = ref(null)

const selectedClientId = ref('')
const showDeleteModal = ref(false)
const transactionToDelete = ref(null)

onMounted(async () => {
  try {
    await clientStore.fetchAll()
  } catch (err) {
    error.value = 'Error al cargar los clientes'
  }
})

const loadTransactionsByClient = async () => {
  if (!selectedClientId.value) return

  try {
    error.value = null
    await transactionStore.fetchByClient(selectedClientId.value)
  } catch (err) {
    error.value = err.response?.data?.message || 'Error al cargar transacciones'
  }
}

const viewTransaction = (id) => {
  router.push(`/transactions/${id}/view`)
}

const editTransaction = (id) => {
  router.push(`/transactions/${id}/edit`)
}

const deleteTransaction = (id) => {
  transactionToDelete.value = id
  showDeleteModal.value = true
}

const confirmDelete = async () => {
  if (!transactionToDelete.value) return

  isDeleting.value = true
  try {
    await transactionStore.deleteTransaction(transactionToDelete.value)
    showDeleteModal.value = false
    transactionToDelete.value = null
    error.value = null
  } catch (err) {
    error.value = 'Error al eliminar la transacción'
  } finally {
    isDeleting.value = false
  }
}
</script>
