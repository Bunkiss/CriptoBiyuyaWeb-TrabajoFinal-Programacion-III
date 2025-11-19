import axios from 'axios'

export const postClient = async (clientData) => {
  try {
    const response = await axios.post('https://localhost:7261/api/Client', clientData)
    return response.data
  } catch (error) {
    console.error('Error al crear el cliente:', error)
    throw error
  }
}
