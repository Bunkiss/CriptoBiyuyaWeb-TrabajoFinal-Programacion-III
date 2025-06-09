<template>
  <div class="conteiner">
    <Header class="header"></Header>
    <RouterView class="main"></RouterView>
    <Footer class="footer"></Footer>
  </div>
</template>

<script setup>
import { RouterView } from 'vue-router'
import Header from '@/components/HeaderComponent.vue'
import Footer from '@/components/FooterComponent.vue'
</script>

<style scoped>

</style>
