<template>
  <Header></Header>
  <RouterView></RouterView>
  <Footer></Footer>
</template>

<script setup>
import { RouterView } from 'vue-router'
import Header from '@/components/HeaderComponent.vue'
import Footer from '@/components/FooterComponent.vue'
</script>

<style scoped>

</style>
