import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { clientApi } from '../api/client.api.js'

export const useClientStore = defineStore('clients', () => {
  const clients = ref([])
  const loading = ref(false)
  const error = ref(null)

  const sortedClients = computed(() =>
    [...clients.value].sort((a, b) => a.name.localeCompare(b.name))
  )

  const fetchAll = async () => {
    loading.value = true
    error.value = null
    try {
      clients.value = await clientApi.getAll()
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al cargar clientes'
      throw err
    } finally {
      loading.value = false
    }
  }

  const getById = async (id) => {
    try {
      return await clientApi.getById(id)
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al obtener cliente'
      throw err
    }
  }

  const create = async (dto) => {
    loading.value = true
    error.value = null
    try {
      const created = await clientApi.create(dto)
      clients.value.push(created)
      return created
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al crear cliente'
      throw err
    } finally {
      loading.value = false
    }
  }

  const update = async (id, dto) => {
    error.value = null
    try {
      await clientApi.update(id, dto)
      const index = clients.value.findIndex((c) => c.id === id)
      if (index !== -1) {
        clients.value[index] = { ...clients.value[index], ...dto }
      }
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al actualizar cliente'
      throw err
    }
  }

  const deleteClient = async (id) => {
    error.value = null
    try {
      await clientApi.delete(id)
      clients.value = clients.value.filter((c) => c.id !== id)
    } catch (err) {
      error.value = err.response?.data?.message || 'Error al eliminar cliente'
      throw err
    }
  }

  return {
    clients,
    sortedClients,
    loading,
    error,
    fetchAll,
    getById,
    create,
    update,
    deleteClient,
  }
})
