<template>
  <div class="overflow-x-auto">
    <table class="w-full text-sm">
      <thead class="bg-gray-100 border-b border-gray-200">
        <tr>
          <th class="px-4 py-3 text-left font-medium text-gray-700">Fecha</th>
          <th class="px-4 py-3 text-left font-medium text-gray-700">Criptomoneda</th>
          <th class="px-4 py-3 text-left font-medium text-gray-700">Tipo</th>
          <th class="px-4 py-3 text-right font-medium text-gray-700">Cantidad</th>
          <th class="px-4 py-3 text-right font-medium text-gray-700">Monto (ARS)</th>
          <th class="px-4 py-3 text-center font-medium text-gray-700">Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="transactions.length === 0" class="border-b border-gray-200">
          <td colspan="7" class="px-4 py-8 text-center text-gray-400">
            No hay transacciones registradas
          </td>
        </tr>
        <tr v-for="transaction in transactions" :key="transaction.id" class="border-b border-gray-200 hover:bg-gray-50">
          <td class="px-4 py-3">{{ formatDateTime(transaction.datetime) }}</td>
          <td class="px-4 py-3">
            <span class="inline-block bg-blue-100 text-blue-800 px-2 py-1 rounded text-xs font-medium">
              {{ formatCryptoName(transaction.crypto_code) }}
            </span>
          </td>
          <td class="px-4 py-3">
            <span
              :class="[
                'inline-block px-2 py-1 rounded text-xs font-medium',
                transaction.action === 'purchase' 
                  ? 'bg-green-100 text-green-800' 
                  : 'bg-red-100 text-red-800',
              ]"
            >
              {{ formatAction(transaction.action) }}
            </span>
          </td>
          <td class="px-4 py-3 text-right">{{ formatCryptoAmount(transaction.crypto_amount) }}</td>
          <td class="px-4 py-3 text-right font-bold">{{ formatMoney(transaction.money) }}</td>
          <td class="px-4 py-3">
            <div class="flex justify-center gap-2">
              <button
                type="button"
                class="px-3 py-1.5 bg-blue-100 text-blue-700 font-medium rounded-lg 
                      hover:bg-blue-200 cursor-pointer transition-colors"
                title="Ver"
                @click="$emit('view', transaction.id)"
              >
                Detalles
              </button>

              <button
                type="button"
                class="px-3 py-1.5 bg-yellow-100 text-yellow-700 font-medium rounded-lg 
                      hover:bg-yellow-200 cursor-pointer transition-colors"
                title="Editar"
                @click="$emit('edit', transaction.id)"
              >
                Editar
              </button>

              <button
                type="button"
                class="px-3 py-1.5 bg-red-100 text-red-700 font-medium rounded-lg 
                      hover:bg-red-200 cursor-pointer transition-colors"
                title="Eliminar"
                @click="$emit('delete', transaction.id)"
              >
                Eliminar
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { useFormatTransaction } from '../composables/useFormatTransaction'

defineProps({
  transactions: {
    type: Array,
    required: true,
  },
})

defineEmits(['view', 'edit', 'delete'])

const { formatDateTime, formatCryptoName, formatAction, formatMoney, formatCryptoAmount } =
  useFormatTransaction()
</script>
