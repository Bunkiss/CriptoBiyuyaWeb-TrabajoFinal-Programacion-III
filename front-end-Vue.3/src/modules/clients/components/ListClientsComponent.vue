<template>
  <div>
    <label for="client">Seleccionar cliente:</label>
    <select id="client" v-model="clientStore.selectedClientId">
      <option disabled value="">-- Seleccione un cliente --</option>
      <option v-for="client in clientStore.clients" :key="client.id" :value="client.id">
        {{ client.name }}
      </option>
    </select>
    <!--Los links te envían a TransactionView en donde el select de cliente estará preseleccionado
    según el cliente seleccionado en la vista actual-->
    <!--<RouterLink to="/transaction" :client_id="client.id :action="purchase">Nueva Compra</RouterLink>-->
    <!--<RouterLink to="/transaction" :client_id="client.id :action="sale">Nueva Venta</RouterLink>-->
    <!--A checkear que esa sea la sintaxis correcta. Lo que se busca es heredar los valores a traves de defineProps-->
  </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { useClientStore } from '@/stores/clientStore';
import { useClients } from '@/composables/useClients';

const clientStore = useClientStore();

onMounted(async () => {
  if (clientStore.clients.length === 0) {
    await useClients();
  }
});
</script>
