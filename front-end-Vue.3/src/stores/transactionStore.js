import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useTransactionStore = defineStore('transaction', () => {
  const transaction = ref(null);
  const resultado = ref(null); // Resultado del post: { money, datetime }
  const enviado = ref(false);

  const setCompra = (data) => {
    transaction.value = data;
  };

  const setResultado = ({ money, datetime }) => {
    resultado.value = { money, datetime };
  };

  const marcarComoEnviado = () => {
    enviado.value = true;
  };

  const resetear = () => {
    transaction.value = null;
    resultado.value = null;
    enviado.value = false;
  };

  return {
    transaction,
    resultado,
    enviado,
    setCompra,
    setResultado,
    marcarComoEnviado,
    resetear
  };
});

