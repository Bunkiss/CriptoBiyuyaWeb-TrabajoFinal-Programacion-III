<template>
  <main class="transaction-view">
    <TransactionComponent
      :client-id="clientId"
      :action="action"
    />
  </main>
</template>

<script setup>
import { useRoute } from 'vue-router'
import TransactionComponent from '../components/TransactionComponent.vue'

const route = useRoute()
const clientId = Number(route.query.client_id)
const action = route.query.action
</script>

<style scoped>
.transaction-view {
  padding: 2rem;
}
/*
  Tomar de referencia del formulario de Biblioteca back-end MVC
*/
</style>
