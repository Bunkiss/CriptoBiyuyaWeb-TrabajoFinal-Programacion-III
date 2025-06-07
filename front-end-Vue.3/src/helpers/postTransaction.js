import axios from 'axios';

const api_url = 'https://localhost:7261/api/Transaction';

export const postTransaction = async (transaction) => {
  try {
    const response = await axios.post(api_url, transaction);
    return {
      success: true,
      data: response.data
    };
  } catch (error) {
    throw new Error('Error al realizar la transacción: ' + error);
  }
};
