<template>
  <div v-if="client">
    <h3>Transacciones de {{ client.name }}</h3>
    <table v-if="client.transactions.length">
      <thead>
        <tr>
          <th>Cripto</th>
          <th>Acción</th>
          <th>Monto</th>
          <th>ARS</th>
          <th>Fecha</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="tx in client.transactions" :key="tx.id">
          <td>{{ formatCryptoName(tx.crypto_code) }}</td>
          <td>{{ formatAction(tx.action) }}</td>
          <td>{{ tx.crypto_amount }}</td>
          <td>{{ formatMoney(tx.money) }}</td>
          <td>{{ formatDate(tx.datetime) }}</td>
          <!--boton VER-->
          <!--boton EDITAR-->
          <!--boton BORRAR-->
        </tr>
      </tbody>
    </table>
    <p v-else>El cliente no tiene transaciones realizadas.</p>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import { useClientStore } from '@/stores/clientStore';
import { useFormatTransaction } from '@/composables/useFormatTransaction'

// Carga de datos
const store = useClientStore();
const client = computed(() => store.selectedClient);

// Formato de datos
const {
  formatMoney,
  formatDate,
  formatCryptoName,
  formatAction,
} = useFormatTransaction()
</script>
