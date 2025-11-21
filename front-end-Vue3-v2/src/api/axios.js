import axios from 'axios'

const API_BASE_URL = 'https://localhost:7261/api'

const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
})

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('[API Error]:', error.response?.data?.message || error.message)
    return Promise.reject(error)
  }
)

export default axiosInstance
