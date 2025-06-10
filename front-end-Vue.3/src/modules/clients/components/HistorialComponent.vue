<template>
  <div v-if="client" class="conteiner">
    <h3 class="title">Transacciones de {{ client.name }}</h3>
    <div class="historial-wrapper">
      <table v-if="client.transactions.length" class="historial">
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

<style scoped>
.conteiner {
    background-color: var(--bg-color-1);
    padding: 20px;
    margin-bottom: 20px;
    border-radius: 15px;
    box-shadow: 0 0 10px rgba(0,0,0,0.1);
    width: 100%;
    max-width: 1200px;
}

.title {
    font-size: 28px;
    font-weight: bold;
}

.historial-wrapper {
  max-height: 600px;
  overflow-y: auto;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0,0,0,0.1);
}

.historial {
    width: 100%;
    border-collapse: collapse;
    background-color: var(--card-color);
    color: var(--text-secondary);
    border-radius: 10px;
    overflow: hidden;
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
    margin-top: 10px;
}
    .historial th, .historial td {
        padding: 12px 15px;
        text-align: left;
    }
    .historial thead {
        background-color: var(--text-primary);
        color: var(--card-color);
    }
    .historial th {
        font-weight: bold;
    }
</style>
