// src/modules/compra/components/CompraComponent.vue
<template>
  <Form @submit="onSubmit" :validation-schema="schema" :initial-values="{ crypto_code: 'BTC', crypto_amount: 0.00001, client_id: 1, }"> <!--cliente id por defecto temporalmente-->
    <div>
      <label for="crypto_code">Criptomoneda</label>
      <Field as="select" name="crypto_code" id="crypto_code">
        <option value="BTC">BTC</option>
        <option value="ETH">ETH</option>
        <option value="USDC">USDC</option>
      </Field>
      <ErrorMessage name="crypto_code" />
    </div>

    <div>
      <label>
        <Field type="radio" name="action" value="purchase" /> Comprar
      </label>
      <label>
        <Field type="radio" name="action" value="sale" /> Vender
      </label>
      <ErrorMessage name="action" />
    </div>
<!--Crear el get/use Clients /store
    <div>
      <label for="client_id">Cliente</label>
      <Field as="select" name="client_id">
        <option v-for="cliente in clients" :key="cliente.id" :value="cliente.id">
          {{ cliente.name }}
        </option>
      </Field>
      <ErrorMessage name="client_id" />
    </div>
-->
    <div>
      <label for="client_id">Cliente</label>
      <Field as="select" name="client_id" id="client_id">
        <option value="1">Cliente Test</option>
      </Field>
      <ErrorMessage name="client_id" />
    </div>

    <div>
      <label for="crypto_amount">Cantidad Cripto</label>
      <Field name="crypto_amount" type="number" step="any" id="crypto_amount"/>
      <ErrorMessage name="crypto_amount" />
    </div>
    <!-- Botón para calcular y mostrar resultado -->
    <div>
      <button type="button" @click="calcularConversion">Calcular</button>
      <div v-if="conversionTexto">
        {{ conversionTexto }}
      </div>
    </div>

    <button type="submit" v-if="!success">Enviar</button>
    <div v-else>Enviado con éxito. ARS {{ money }} - {{ datetime }}</div>
  </Form>
</template>

<script setup>
import { Form, Field, ErrorMessage, useForm } from 'vee-validate';
import { ref, onMounted } from 'vue';
// import { useClientesStore } from '@/stores/clientesStore';
import { useTransactions } from '@/composables/useTransactions';
import { format } from 'date-fns'; //
import { schema } from '@/modules/compra/schemas/validationTransactionSchema';

const { realizarCompra } = useTransactions();
// const clients = useClientesStore().data;
const money = ref(0);
const datetime = ref('');
const success = ref(false);

// --- CALCULAR COSTO/GANANCIA ---

const { values } = useForm();
const conversionTexto = ref('');
// const { value: cryptoCode } = useField('crypto_code');
// const { value: cryptoAmount } = useField('crypto_amount');

const calcularConversion = async () => {
  const cryptoCode = values.crypto_code;
  const cryptoAmount = values.crypto_amount;

  if (!cryptoCode || !cryptoAmount) return;

  try {
    const response = await fetch(`https://criptoya.com/api/bybit/${cryptoCode}/ARS/1`);
    const data = await response.json();
    const precioARS = data.totalAsk || Object.values(data)[0];
    const total = (precioARS * cryptoAmount).toFixed(2);

    conversionTexto.value = `${Number(cryptoAmount).toFixed(5)} ${cryptoCode} = ${total} ARS`;
  } catch (error) {
    console.error(error);
    conversionTexto.value = 'Error al calcular el precio.';
  }
};
// -- valor por defecto
onMounted(() => {
  setTimeout(calcularConversion, 50); // mostrar el valor inicial y da tiempo a que se sincronicen los campos
});

// --- POST ---

const onSubmit = async (values) => {  /* values contiene los datos del formulario validados */
  const precio = await fetch(`https://criptoya.com/api/bybit/${values.crypto_code}/ARS/1`).then(r => r.json());
  const precioARS = precio.totalAsk || Object.values(precio)[0];
  money.value = (precioARS * values.crypto_amount).toFixed(2);

  datetime.value = format(new Date(), 'yyyy-MM-dd HH:mm'); //new Date() crea un objeto de fecha y hora actual del sistema y format() lo hace uns tring legible

  const body = {
    ...values,            //este spread ... copia todas las propiedades de values
    money: money.value,
    datetime: datetime.value
  };

  await realizarCompra(body);
  success.value = true;
};
</script>

<style scoped>
/* Estilos mínimos */
div { margin-bottom: 1rem; }
label { display: block; margin-bottom: 0.3rem; }
input, select { width: 100%; padding: 0.4rem; }
button { padding: 0.5rem 1rem; background: #333; color: white; border: none; border-radius: 5px; }
</style>
