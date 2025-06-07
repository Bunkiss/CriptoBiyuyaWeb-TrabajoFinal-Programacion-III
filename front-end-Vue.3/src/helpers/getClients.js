import axios from 'axios';

const api_url = 'https://localhost:7261/api/Client';

export const getClients = async () => {
  const response = await axios.get(api_url);
  return response.data;
};
