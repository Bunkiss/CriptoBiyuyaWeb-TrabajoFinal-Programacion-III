<template>
  <div class="transaction-form">
    <h2 class="text-xl mb-4">
      {{ isPurchase ? 'Nueva Compra' : 'Nueva Venta' }}
    </h2>

    <!-- Formulario validado con vee-validate -->
    <Form :validation-schema="validationSchema" @submit="onSubmit">
      <!-- Selección de criptomoneda -->
      <div class="mb-4">
        <label>Criptomoneda:</label>
        <Field name="crypto_code" as="select" class="border p-2 w-full" v-model="cryptoCode">
          <option value="">Seleccione una opción</option>
          <option value="btc">{{formatCryptoName('btc')}}</option>
          <option value="eth">{{formatCryptoName('eth')}}</option>
          <option value="usdc">{{formatCryptoName('usdc')}}</option>
        </Field>
        <ErrorMessage name="crypto_code" class="text-red-500 text-sm" />
      </div>

      <!-- Ingreso de cantidad -->
      <div class="mb-4">
        <label>Cantidad:</label>
        <Field
          name="crypto_amount"
          type="number"
          step="0.00001"
          class="border p-2 w-full"
          v-model.number="cryptoAmount"
        />
        <ErrorMessage name="crypto_amount" class="text-red-500 text-sm" />
      </div>

      <!-- Selección de cliente -->
      <div class="mb-4">
        <label>Cliente:</label>
        <Field name="client_id" as="select" class="border p-2 w-full">
          <option v-for="client in clientStore.clients" :key="client.id" :value="client.id">
            {{ client.name }}
          </option>
        </Field>
        <ErrorMessage name="client_id" class="text-red-500 text-sm" />
      </div>

      <!-- Botón para calcular el valor de la transacción -->
      <div class="mb-4">
        <button
          type="button"
          class="bg-gray-300 hover:bg-gray-400 text-black px-4 py-2 rounded"
          @click="convertirCrypto"
          :disabled="!cryptoCode || !cryptoAmount"
        >
          Calcular valor en ARS
        </button>
      </div>

      <!-- Valor estimado calculado -->
      <div class="mb-4" v-if="money">
        <p class="text-gray-700">
          Valor en ARS: <strong>{{ formattedMoney }}</strong>
        </p>
      </div>

      <!-- Botón de envío -->
      <button
      type="submit"
      class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded"
      >
      {{ isPurchase ? 'Comprar' : 'Vender' }}
    </button>

    <!-- Confirmación visual luego de enviar -->
    <div v-if="showConfirmation" class="p-4 bg-green-100 text-green-700 rounded mb-4">
      Transacción registrada por {{ formattedMoney }} a las {{ formattedDatetime }}
    </div>

    </Form>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { Form, Field, ErrorMessage, useForm } from 'vee-validate'
import * as yup from 'yup'
import { useTransaction } from '@/composables/useTransactions'
import { useClients } from '@/composables/useClients'
import { useClientStore } from '@/stores/clientStore'
import axios from 'axios'
import { useFormatTransaction } from '@/composables/useFormatTransaction'

// Props heredados desde el router (cliente y acción)
const props = defineProps({
  action: {
    type: String,
    required: true,
    default: 'purchase',
    validator: (v) => ['purchase', 'sale'].includes(v),
  },
  client_id: {
    type: Number,
    default: null,
  },
})

// Flags y estados reactivos
const isPurchase = computed(() => props.action === 'purchase')
const clientStore = useClientStore()
const { createTransaction } = useTransaction()
const money = ref(0)                // ARS calculado
const datetime = ref('')           // Fecha y hora de envío
const showConfirmation = ref(false) // Mensaje luego del POST
const cryptoCode = ref('')
const cryptoAmount = ref(null)

// Esquema de validación con yup
const validationSchema = yup.object({
  crypto_code: yup.string().required('La criptomoneda es obligatoria'),
  crypto_amount: yup
    .number()
    .required('La cantidad es obligatoria')
    .min(0.00001, 'La cantidad debe ser mayor a 0.00001'),
  client_id: yup.number().required('Debe seleccionar un cliente'),
})

// Referencia al formulario y sus valores
const { setFieldValue } = useForm()

// Formato para datetime y money
const { formatMoney, formatDate, formatCryptoName } = useFormatTransaction()
const formattedMoney = computed(() => formatMoney(money.value))
const formattedDatetime = computed(() => formatDate(datetime.value))

// Función para calcular el valor en ARS cuando el usuario hace clic
const convertirCrypto = async () => {
  if (!cryptoCode.value || !cryptoAmount.value) {
    console.warn('Faltan datos para calcular.')
    return
  }

  try {
    const res = await axios.get(
      `https://criptoya.com/api/satoshitango/${cryptoCode.value}/ars`
    )
    const price = res.data.totalBid ?? res.data.ask ?? res.data.price
    money.value = cryptoAmount.value * price

    console.log(`Precio unitario: ${price}, Cantidad: ${cryptoAmount.value}, Total ARS: ${money.value}`)
  } catch (err) {
    console.error('Error al obtener el precio:', err)
  }
}

// Auto set del cliente si viene por props    -   No esta seteando
onMounted(async () => {
  await useClients()
  if (props.client_id) {
    setFieldValue('client_id', props.client_id)
  }
})

// Función al enviar el formulario
const onSubmit = async (values) => {
  const now = new Date()
  datetime.value = now.toISOString()

  const payload = {
    ...values,
    action: props.action,
    money: money.value,
    datetime: datetime.value,
  }

  console.log('Payload a enviar:', payload)

  try {
    await createTransaction(payload)
    showConfirmation.value = true
  } catch (err) {
    console.error('Error al crear transacción:', err)
  }
}
</script>

<style scoped>
.transaction-form {
  max-width: 500px;
  margin: auto;
}
</style>
