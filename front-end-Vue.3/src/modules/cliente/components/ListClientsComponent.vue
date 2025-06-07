<template>
  <div>
    <label for="client">Seleccionar cliente:</label>
    <select id="client" v-model="clientStore.selectedClientId">
      <option disabled value="">-- Seleccione un cliente --</option>
      <option v-for="client in clientStore.clients" :key="client.id" :value="client.id">
        {{ client.name }}
      </option>
    </select>
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
