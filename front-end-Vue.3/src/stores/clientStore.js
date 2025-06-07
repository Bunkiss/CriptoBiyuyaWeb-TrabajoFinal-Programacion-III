import { defineStore } from 'pinia';
import { ref, computed } from 'vue';

export const useClientStore = defineStore('clientStore', () => {
  const clients = ref([]);            // Almacena la lista de clientes traídos desde la API
  const selectedClientId = ref(null); // Almacena el ID del cliente seleccionado desde el <select>

  // Permite setear la lista de clientes (usado por el composable)
  const setClients = (data) => {
    clients.value = data;
  };

  // Permite setear el cliente actualmente seleccionado
  const setSelectedClientId = (id) => {
    selectedClientId.value = id;
  };

  // Devuelve el objeto cliente completo según el ID seleccionado
  const selectedClient = computed(() =>
    clients.value.find(c => c.id === selectedClientId.value)
  );

  return {
    clients,
    selectedClientId,
    selectedClient,
    setClients,
    setSelectedClientId
  };
});
