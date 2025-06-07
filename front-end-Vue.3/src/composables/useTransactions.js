import { useTransactionStore } from '@/stores/transactionStore';
import { postTransaction } from '@/helpers/postTransaction';

export const useTransactions = () => {
  const transactionStore = useTransactionStore();

  const realizarCompra = async (formData) => {
    // Guardamos datos en el store
    transactionStore.setCompra(formData);

    // Hacemos el post
    const response = await postTransaction(formData);

    if (response.success) {
      transactionStore.setResultado(response.data);
      transactionStore.marcarComoEnviado();
    } else {
      console.error('Error al realizar la compra');
    }
  };

  return {
    realizarCompra
  };
};
