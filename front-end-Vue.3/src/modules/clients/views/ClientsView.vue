<template>
  <div class="conteiner">
    <ListClientsComponent />
    <HistorialComponent />
  </div>
</template>

<script setup>
import ListClientsComponent from '../components/ListClientsComponent.vue';
import HistorialComponent from '../components/HistorialComponent.vue';
</script>

<style scoped>
.conteiner {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
</style>
