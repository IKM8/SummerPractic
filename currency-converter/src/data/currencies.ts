import type { Currency } from '../types/currency';

export const currencies: Currency[] = [
  {
    code: 'PLN',
    title: 'Polish zloty',
    symbol: 'zł',
    description: 'The Polish zloty is the official currency of Poland. It is issued by the National Bank of Poland.'
  },
  {
    code: 'JPY',
    title: 'Japanese yen',
    symbol: '¥',
    description:
      'The Japanese yen is the official currency of Japan. It is the third most traded currency in the foreign exchange market.'
  },
  {
    code: 'CAD',
    title: 'Canadian dollar',
    symbol: '$',
    description: 'The Canadian dollar is the official currency of Canada. It is often referred to as the loonie.'
  },
  {
    code: 'USD',
    title: 'United States dollar',
    symbol: '$',
    description: 'The United States dollar is the official currency of the United States of America.'
  },
  {
    code: 'EUR',
    title: 'Euro',
    symbol: '€',
    description: 'The euro is the official currency of the eurozone, used by most countries of the European Union.'
  }
];
