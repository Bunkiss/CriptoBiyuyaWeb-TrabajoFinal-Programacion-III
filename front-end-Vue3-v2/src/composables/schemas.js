import * as yup from 'yup'

export const clientSchema = yup.object({
  name: yup.string().required('El nombre es requerido').min(3, 'Mínimo 3 caracteres'),
  email: yup
    .string()
    .required('El email es requerido')
    .email('Formato de email inválido'),
})

export const transactionSchema = yup.object({
  clientId: yup.number().required('El cliente es requerido').positive(),
  cryptoCode: yup.string().required('La criptomoneda es requerida').oneOf(['btc', 'eth', 'usdc']),
  cryptoAmount: yup
    .number()
    .required('La cantidad es requerida')
    .positive('La cantidad debe ser mayor a 0'),
  datetime: yup.string().required('La fecha y hora son requeridas'),
})

export const transactionEditSchema = yup.object({
  clientId: yup.number().required('El cliente es requerido').positive(),
  cryptoCode: yup.string().required('La criptomoneda es requerida').oneOf(['btc', 'eth', 'usdc']),
  cryptoAmount: yup
    .number()
    .required('La cantidad es requerida')
    .positive('La cantidad debe ser mayor a 0'),
  money: yup
    .number()
    .required('El monto es requerido')
    .positive('El monto debe ser mayor a 0'),
  datetime: yup.string().required('La fecha y hora son requeridas'),
  action: yup.string().required('El tipo es requerido').oneOf(['purchase', 'sale']),
})
