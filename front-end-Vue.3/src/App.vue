<template>
  <div class="app-container">
    <Header />
    <main class="main-content">
      <RouterView />
    </main>
    <Footer />
  </div>
</template>

<script setup>
import { RouterView } from 'vue-router'
import Header from '@/components/HeaderComponent.vue'
import Footer from '@/components/FooterComponent.vue'
</script>

<style scoped>
html, body, #app {
  height: 100%;
  margin: 0;
}

.app-container {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.main-content {
  flex-grow: 1; /* ocupa todo el espacio disponible */
  overflow-y: auto;
}
</style>
