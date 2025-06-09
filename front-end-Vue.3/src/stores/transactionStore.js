import { defineStore } from 'pinia'
import { postTransaction } from '@/helpers/postTransaction'

export const useTransactionStore = defineStore('transactionStore', {
  state: () => ({
    transactions: [],
  }),
  actions: {
    async createTransaction(data) {
      const nueva = await postTransaction(data)
      this.transactions.push(nueva)
    },
  },
})
