import * as yup from 'yup';

export const schema = yup.object({
  crypto_code: yup.string().required(),
  action: yup.string().oneOf(['purchase', 'sale']).required(),
  client_id: yup.number().required(),
  crypto_amount: yup.number().positive().required()
});
