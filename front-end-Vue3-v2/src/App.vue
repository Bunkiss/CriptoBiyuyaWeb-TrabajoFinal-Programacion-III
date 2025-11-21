<template>
  <div class="flex flex-col min-h-screen bg-gray-50">
    <nav class="sticky top-0 z-50 bg-gray-900 text-white shadow-lg">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between items-center h-16">
          <RouterLink to="/" class="flex items-center gap-2 font-bold text-xl">
            💰 CriptoBiyuya
          </RouterLink>
          <div class="hidden md:flex gap-8">
            <RouterLink to="/clients/create" class="hover:text-blue-400 transition-colors flex items-center gap-1">
              👤 Nuevo Cliente
            </RouterLink>
            <RouterLink to="/transactions/purchase" class="hover:text-blue-400 transition-colors flex items-center gap-1">
              🛒 Nueva Compra
            </RouterLink>
            <RouterLink to="/transactions/sale" class="hover:text-blue-400 transition-colors flex items-center gap-1">
              🏷️ Nueva Venta
            </RouterLink>
            <RouterLink to="/transactions/history" class="hover:text-blue-400 transition-colors flex items-center gap-1">
              📋 Historial
            </RouterLink>
          </div>
          <button @click="toggleMenu" class="md:hidden text-white cursor-pointer">
            ☰
          </button>
        </div>

        <div v-show="menuOpen" class="md:hidden pb-4 space-y-2">
          <RouterLink to="/clients/create" class="block py-2 hover:text-blue-400">
            👤 Nuevo Cliente
          </RouterLink>
          <RouterLink to="/transactions/purchase" class="block py-2 hover:text-blue-400">
            🛒 Nueva Compra
          </RouterLink>
          <RouterLink to="/transactions/sale" class="block py-2 hover:text-blue-400">
            🏷️ Nueva Venta
          </RouterLink>
          <RouterLink to="/transactions/history" class="block py-2 hover:text-blue-400">
            📋 Historial
          </RouterLink>
        </div>
      </div>
    </nav>
  
    <main class="flex-1 bg-gray-50">
      <RouterView />
    </main>
  
    <footer class="bg-gray-900 text-white border-t border-gray-800 py-6">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center text-gray-400">
        <p class="mb-0">
          © 2025 CriptoBiyuya - Gestión de Transacciones de Criptomonedas
        </p>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { ref } from 'vue'

const menuOpen = ref(false)

const toggleMenu = () => {
  menuOpen.value = !menuOpen.value
}
</script>
