<template>
  <div v-if="client">
    <h3>Transacciones de {{ client.name }}</h3>
    <table v-if="client.transactions.length">
      <thead>
        <tr>
          <th>Cripto</th>
          <th>Acción</th>
          <th>Monto</th>
          <th>Dinero</th>
          <th>Fecha</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="tx in client.transactions" :key="tx.id">
          <td>{{ tx.crypto_code }}</td>
          <td>{{ tx.action }}</td>
          <td>{{ tx.crypto_amount }}</td>
          <td>{{ tx.money }}</td>
          <td>{{ tx.datetime }}</td>
        </tr>
      </tbody>
    </table>
    <p v-else>No hay transacciones para este cliente.</p>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import { useClientStore } from '@/stores/clientStore';

const store = useClientStore();
const client = computed(() => store.selectedClient);
</script>
