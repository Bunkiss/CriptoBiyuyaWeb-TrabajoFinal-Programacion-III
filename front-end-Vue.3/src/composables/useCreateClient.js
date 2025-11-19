import { postClient } from '@/helpers/postClient'
import { useClientStore } from '@/stores/clientStore'

export const useCreateClient = () => {
  const clientStore = useClientStore()

  const createClient = async (clientData) => {
    const newClient = await postClient(clientData)
    clientStore.addClient(newClient)
    return newClient
  }

  return { createClient }
}
