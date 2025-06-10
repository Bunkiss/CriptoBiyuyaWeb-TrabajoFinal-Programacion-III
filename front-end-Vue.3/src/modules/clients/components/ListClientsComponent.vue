<template>
  <div class="conteiner">
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
        class="btn"
      >
        Nueva Compra
      </RouterLink>

      <RouterLink
        :to="{
          name: 'Transaction',
          query: { client_id: clientStore.selectedClientId, action: 'sale' },
        }"
        class="btn"
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
.conteiner {
  background-color: var(--bg-color-1);
  margin: 25px;
  border-radius: 15px;
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
  width: 100%;
  max-width: 1200px;
  height: 150px;
}

label {
  display: block;
  font-weight: bold;
  margin-bottom: 6px;
  color: var(--text-secondary);
}

input[type="number"],
select {
  padding: 10px 12px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 1rem;
  background-color: var(--bg-color-1);
  color: var(--success-color);
  transition: border-color 0.2s ease, box-shadow 0.2s ease;
}
input[type="number"]:focus,
select:focus {
  border-color: var(--success-color);
  outline: none;
  box-shadow: 0 0 0 2px rgba(53, 224, 10, 0.3);
}

.btn {
  padding: 8px 16px;
  margin: 10px 5px;
  font-size: 1rem;
  background-color: var(--success-color);
  color: var(--bg-color-1);
  text-decoration: none;
  border-radius: 6px;
  font-weight: bold;
  transition: background-color 0.3s ease;
  box-shadow: 0 6px 8px rgba(0, 0, 0, 0.664);
  min-width: 90px;
  text-align: center;
  border: none;
}
.btn:hover {
  background-color: rgb(53, 224, 10);
}
</style>

