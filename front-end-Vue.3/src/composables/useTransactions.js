import { useTransactionStore } from '@/stores/transactionStore'

export const useTransaction = () => {
  const store = useTransactionStore()

  const createTransaction = async (transactionData) => {
    await store.createTransaction(transactionData)
  }

  return {
    createTransaction,
  }
}
