import axios from 'axios';

export const getTransactionsByClientId = async (clientId) => {
  try {
    const response = await axios.get(`https://localhost:7261/api/Transaction/byClient/${clientId}`);
    return response.data;
  } catch (error) {
    console.error('Error al obtener transacciones:', error);
    throw error;
  }
};
