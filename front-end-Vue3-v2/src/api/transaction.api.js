import axiosInstance from './axios.js'

export const transactionApi = {
  getAll: async () => {
    const { data } = await axiosInstance.get('/transaction')
    return data
  },

  getById: async (id) => {
    const { data } = await axiosInstance.get(`/transaction/${id}`)
    return data
  },

  getByClient: async (clientId) => {
    const { data } = await axiosInstance.get(`/transaction/by-client/${clientId}`)
    return data
  },

  create: async (dto) => {
    const { data } = await axiosInstance.post('/transaction', dto)
    return data
  },

  update: async (id, dto) => {
    await axiosInstance.put(`/transaction/${id}`, dto)
  },

  patch: async (id, dto) => {
    const { data } = await axiosInstance.patch(`/transaction/${id}`, dto)
    return data
  },

  delete: async (id) => {
    await axiosInstance.delete(`/transaction/${id}`)
  },
}
