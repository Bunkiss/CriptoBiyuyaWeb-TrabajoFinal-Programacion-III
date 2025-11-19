<template>
  <div class="form">
    <h2 class="title">Registrar nuevo cliente</h2>

    <Form :validation-schema="validationSchema" @submit="onSubmit">
      <!-- Nombre -->
      <div class="mb-4">
        <label for="name">Nombre:</label>
        <Field name="name" type="text" id="name" class="field" />
        <ErrorMessage name="name" class="error" />
      </div>

      <!-- Email -->
      <div class="mb-4">
        <label for="email">Email:</label>
        <Field name="email" type="email" id="email" class="field" />
        <ErrorMessage name="email" class="error" />
      </div>

      <!-- Botón de envío -->
      <button type="submit" class="btn-submit">Guardar Cliente</button>
    </Form>

    <!-- Confirmación -->
    <div v-if="successMessage" class="success">
      Cliente creado: {{ successMessage.name }} ({{ successMessage.email }})
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { Form, Field, ErrorMessage } from 'vee-validate'
import { validationClientSchema } from '@/modules/clients/schemas/validationClientSchema'
import { useCreateClient } from '@/composables/useCreateClient'

const { createClient } = useCreateClient()
const validationSchema = validationClientSchema

const successMessage = ref(null)

const onSubmit = async (values, { resetForm }) => {
  try {
    const newClient = await createClient(values)
    successMessage.value = newClient
    resetForm()
  } catch (err) {
    console.error('Error al crear cliente:', err)
  }
}
</script>

<style scoped>
.form {
  background-color: var(--bg-color-1);
  padding: 20px 30px;
  margin: 25px auto;
  border-radius: 15px;
  box-shadow: 0 8px 10px rgba(0, 0, 0, 0.1);
  max-width: 500px;
}
.title {
  text-align: center;
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 20px;
}
label {
  display: block;
  font-weight: bold;
  margin-bottom: 6px;
  color: var(--text-secondary);
}
.field {
  width: 100%;
  padding: 10px 12px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 1rem;
  background-color: var(--bg-color-1);
  color: var(--success-color);
}
.error {
  color: red;
  font-size: 0.9rem;
  margin-top: 4px;
}
.btn-submit {
  padding: 10px 16px;
  margin-top: 20px;
  font-size: 1rem;
  background-color: var(--success-color);
  color: var(--bg-color-1);
  border-radius: 6px;
  font-weight: bold;
  border: none;
  cursor: pointer;
}
.success {
  margin-top: 20px;
  background: #dcfce7;
  color: #15803d;
  padding: 10px;
  border-radius: 6px;
  font-weight: bold;
}
</style>
