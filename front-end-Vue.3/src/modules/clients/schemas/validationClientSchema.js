import * as yup from 'yup'

export const validationClientSchema = yup.object({
  name: yup
    .string()
    .required('El nombre es obligatorio')
    .min(2, 'Debe tener al menos 2 caracteres'),
  email: yup
    .string()
    .required('El email es obligatorio')
    .email('Debe ser un email válido'),
})
