import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { transactionApi } from '../api/transaction.api.js'

export const useTransactionStore = defineStore('transactions', () => {
  const transactions = ref([])
  const loading = ref(false)
  const error = ref(null)

  const sortedByDate = computed(() =>
    [...transactions.value].sort(
      (a, b) => new Date(b.datetime).getTime() - new Date(a.datetime).getTime()
    )
  )

  const fetchAll = async () => {
    loading.value = true
    error.value = null
    try {
      transactions.value = await transactionApi.getAll()
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al cargar transacciones'
      throw err
    } finally {
      loading.value = false
    }
  }

  const fetchByClient = async (clientId) => {
    loading.value = true
    error.value = null
    try {
      transactions.value = await transactionApi.getByClient(clientId)
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al cargar transacciones del cliente'
      throw err
    } finally {
      loading.value = false
    }
  }

  const getById = async (id) => {
    try {
      return await transactionApi.getById(id)
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al obtener transacción'
      throw err
    }
  }

  const create = async (dto) => {
    loading.value = true
    error.value = null
    try {
      const created = await transactionApi.create(dto)
      transactions.value.push(created)
      return created
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al crear transacción'
      throw err
    } finally {
      loading.value = false
    }
  }

  const update = async (id, dto) => {
    error.value = null
    try {
      await transactionApi.update(id, dto)
      const index = transactions.value.findIndex((t) => t.id === id)
      if (index !== -1) {
        transactions.value[index] = { ...transactions.value[index], ...dto }
      }
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al actualizar transacción'
      throw err
    }
  }

  const patch = async (id, dto) => {
    error.value = null
    try {
      const updated = await transactionApi.patch(id, dto)
      const index = transactions.value.findIndex((t) => t.id === id)
      if (index !== -1) {
        transactions.value[index] = updated
      }
      return updated
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al actualizar transacción'
      throw err
    }
  }

  const deleteTransaction = async (id) => {
    error.value = null
    try {
      await transactionApi.delete(id)
      transactions.value = transactions.value.filter((t) => t.id !== id)
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al eliminar transacción'
      throw err
    }
  }

  return {
    transactions,
    sortedByDate,
    loading,
    error,
    fetchAll,
    fetchByClient,
    getById,
    create,
    update,
    patch,
    deleteTransaction,
  }
})
