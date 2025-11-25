<template>
  <form @submit.prevent="onSubmit" class="space-y-4">

    <div>
      <label for="name" class="block text-sm font-medium text-gray-700 mb-1">
        Nombre y Apellido
      </label>

      <input
        id="name"
        v-model="name"
        type="text"
        class="w-full px-3 py-2 border border-gray-300 rounded-lg 
               focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
        :class="{ 'border-red-500': errors.name }"
        placeholder="Ingrese el nombre completo"
      />

      <div v-if="errors.name" class="text-red-600 text-sm mt-1">
        {{ errors.name }}
      </div>
    </div>

    <div>
      <label for="email" class="block text-sm font-medium text-gray-700 mb-1">
        Email
      </label>

      <input
        id="email"
        v-model="email"
        type="email"
        class="w-full px-3 py-2 border border-gray-300 rounded-lg 
               focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
        :class="{ 'border-red-500': errors.email }"
        placeholder="correo@ejemplo.com"
      />

      <div v-if="errors.email" class="text-red-600 text-sm mt-1">
        {{ errors.email }}
      </div>
    </div>

    <div v-if="apiError" class="p-3 bg-red-50 border border-red-200 text-red-700 rounded-lg text-sm">
      {{ apiError }}
    </div>

    <button
      type="submit"
      :disabled="isLoading"
      class="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium 
             py-2 px-4 rounded-lg transition-colors 
             disabled:opacity-50 disabled:cursor-not-allowed
             flex items-center justify-center gap-2 cursor-pointer"
    >
      {{ isLoading ? '⏳ Creando...' : 'Crear Cliente' }}
    </button>

  </form>
</template>

<script setup>
import { ref } from 'vue'
import { useForm, useField } from 'vee-validate'
import { clientSchema } from '../composables/schemas'
import { useClientStore } from '../stores/clientStore'
import { useRouter } from 'vue-router'

const router = useRouter()
const clientStore = useClientStore()

const { handleSubmit, errors } = useForm({
  validationSchema: clientSchema,
})

const { value: name } = useField('name')
const { value: email } = useField('email')

const isLoading = ref(false)
const apiError = ref(null)

const onSubmit = handleSubmit(async (values) => {
  isLoading.value = true
  apiError.value = null

  try {
    await clientStore.create({
      name: values.name,
      email: values.email,
    })

    router.push('/transactions/history')
  } catch (err) {
    apiError.value =
      err.response?.data?.message || 'Error al crear el cliente'
  } finally {
    isLoading.value = false
  }
})
</script>
