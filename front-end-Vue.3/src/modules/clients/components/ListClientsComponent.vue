<template>
  <div>
    <label for="client">Seleccionar cliente:</label>
    <select id="client" v-model="clientStore.selectedClientId">
      <option disabled value="">-- Seleccione un cliente --</option>
      <option
        v-for="client in clientStore.clients"
        :key="client.id"
        :value="client.id"
      >
        {{ client.name }}
      </option>
    </select>

    <div v-if="clientStore.selectedClientId" style="margin-top: 1rem;">
      <RouterLink
        :to="{
          name: 'Transaction',
          query: { client_id: clientStore.selectedClientId, action: 'purchase' },
        }"
        class="btn purchase"
      >
        Nueva Compra
      </RouterLink>

      <RouterLink
        :to="{
          name: 'Transaction',
          query: { client_id: clientStore.selectedClientId, action: 'sale' },
        }"
        class="btn sale"
      >
        Nueva Venta
      </RouterLink>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { useClientStore } from '@/stores/clientStore';
import { useClients } from '@/composables/useClients';
import { RouterLink } from 'vue-router';

const clientStore = useClientStore();

onMounted(async () => {
  if (clientStore.clients.length === 0) {
    await useClients();
  }
});
</script>

<style scoped>
.btn {
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  text-decoration: none;
  border: none;
  border-radius: 5px;
  font-weight: bold;
  color: white;
  cursor: pointer;
}

.purchase {
  background-color: #28a745; /* verde */
}

.sale {
  background-color: #dc3545; /* rojo */
}
</style>

