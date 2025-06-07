import { getClients } from '@/helpers/getClients';
import { useClientStore } from '@/stores/clientStore';

// Este composable actúa de puente entre el helper (HTTP) y el store (estado global)
export const useClients = async () => {
  const data = await getClients();             // Llama al helper para obtener los datos
  const clientStore = useClientStore();        // Obtiene la instancia del store
  clientStore.setClients(data);                // Setea los clientes en el estado global
};
