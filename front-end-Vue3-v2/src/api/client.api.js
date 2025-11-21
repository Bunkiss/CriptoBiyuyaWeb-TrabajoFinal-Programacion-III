import axiosInstance from './axios.js'

export const clientApi = {
  getAll: async () => {
    const { data } = await axiosInstance.get('/client')
    return data
  },

  getById: async (id) => {
    const { data } = await axiosInstance.get(`/client/${id}`)
    return data
  },

  create: async (dto) => {
    const { data } = await axiosInstance.post('/client', dto)
    return data
  },

  update: async (id, dto) => {
    await axiosInstance.put(`/client/${id}`, dto)
  },

  delete: async (id) => {
    await axiosInstance.delete(`/client/${id}`)
  },
}
