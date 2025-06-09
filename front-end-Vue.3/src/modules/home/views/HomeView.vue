<template>
  <main class="main">
    <section class="conteiner">
      <RouterLink
        class="link"
        to="/historial"
      >
        Historial de Movimientos
      </RouterLink>
    </section>
    <section class="conteiner">
      <RouterLink
        class="link"
        :to="{
          name: 'Transaction',
          query: { action: 'purchase' },
        }"
      >
        Nueva Compra
      </RouterLink>
    </section>
    <section class="conteiner">
      <RouterLink
        class="link"
        :to="{
          name: 'Transaction',
          query: { action: 'sale' },
        }"
      >
        Nueva Venta
      </RouterLink>
    </section>
  </main>
</template>

<script setup>
import { RouterLink } from 'vue-router';
</script>

<style scoped>
.main {
  height: 84vb;
  background-color: var(--surface-color);
  padding: 20px;
}

.conteiner {
  display: flex;
  flex-direction: column;
  align-items: center;
  background-color: var(--bg-color);
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.575);
  max-width: 60vw;
  margin: 0 auto 50px auto;
}

.link {
  padding: 5px 10px;
  color: var(--text-secondary);
  text-decoration: none;
  font-weight: bold;
  width: 100%;
  text-align: center;
  font-size: 20px;
}
.link:hover {
  background-color: var(--success-color);
  color: var(--bg-color);
  border-radius: 5px;
}
</style>
