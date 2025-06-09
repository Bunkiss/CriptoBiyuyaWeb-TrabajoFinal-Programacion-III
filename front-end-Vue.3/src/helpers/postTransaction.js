import axios from 'axios'

export const postTransaction = async (transactionData) => {
  try {
    const response = await axios.post('https://localhost:7261/api/Transaction', transactionData)
    return response.data
  } catch (error) {
    console.error('Error al enviar transacción:', error)
    throw error
  }
}
