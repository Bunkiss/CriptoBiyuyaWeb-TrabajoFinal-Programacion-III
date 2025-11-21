<template>
  <div class="max-w-2xl mx-auto px-4 py-8">
    <div class="bg-white rounded-lg shadow-lg p-6">
      <div class="mb-6 pb-4 border-b border-gray-200">
        <h1 class="text-2xl font-bold text-gray-900">
          <i class="fas fa-user-plus mr-2 text-blue-600"></i>Crear Nuevo Cliente
        </h1>
      </div>
      <CreateClientForm />
    </div>
  </div>
</template>

<script setup>
import CreateClientForm from '../components/CreateClientForm.vue'
</script>
