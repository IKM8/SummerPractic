import type { Currency } from '../types/currency';

export const currencies: Currency[] = [
  {
    code: 'PLN',
    title: 'Polish Zloty',
    symbol: 'zł',
    description: 'The Polish Zloty is the official currency of Poland. It is issued by the National Bank of Poland.'
  },
  {
    code: 'JPY',
    title: 'Japanese Yen',
    symbol: '¥',
    description:
      'The Japanese Yen is the official currency of Japan. It is the third most traded currency in the foreign exchange market.'
  },
  {
    code: 'CAD',
    title: 'Canadian Dollar',
    symbol: 'CA$',
    description: 'The Canadian Dollar is the official currency of Canada. It is often referred to as the loonie.'
  },
  {
    code: 'USD',
    title: 'US Dollar',
    symbol: '$',
    description: 'The US Dollar is the official currency of the United States of America.'
  },
  {
    code: 'RUB',
    title: 'Russian Ruble',
    symbol: '₽',
    description: 'The Russian Ruble is the official currency of the Russian Federation.'
  }
];
