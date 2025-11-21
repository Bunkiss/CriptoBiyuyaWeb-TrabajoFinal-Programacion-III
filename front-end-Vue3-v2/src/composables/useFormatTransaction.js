export const useFormatTransaction = () => {
  const formatMoney = (value) =>
    new Intl.NumberFormat('es-AR', {
      style: 'currency',
      currency: 'ARS',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(value)

  const formatDateTime = (value) => {
    if (!value) return ''
    const date = typeof value === 'string' ? new Date(value) : value
    return date.toLocaleString('es-AR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
      hour12: false,
    })
  }

  const formatCryptoName = (code) => {
    const map = {
      btc: 'Bitcoin (BTC)',
      eth: 'Ethereum (ETH)',
      usdc: 'USD Coin (USDC)',
    }
    return map[code] || code
  }

  const formatAction = (action) => {
    return action === 'purchase' ? 'Compra' : action === 'sale' ? 'Venta' : action
  }

  const formatCryptoAmount = (amount) => {
    return new Intl.NumberFormat('es-AR', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 8,
    }).format(amount)
  }

  return {
    formatMoney,
    formatDateTime,
    formatCryptoName,
    formatAction,
    formatCryptoAmount,
  }
}
