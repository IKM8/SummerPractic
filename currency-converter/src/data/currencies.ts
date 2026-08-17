import type { Currency } from '../types/currency'

export const currencies: Currency[] = [
  { code: 'PLN', title: 'Polish Zloty', symbol: 'zł' },
  { code: 'JPY', title: 'Japanese Yen', symbol: '¥' },
  { code: 'CAD', title: 'Canadian Dollar', symbol: 'CA$' },
  { code: 'USD', title: 'US Dollar', symbol: '$' },
  { code: 'RUB', title: 'Russian Ruble', symbol: '₽' },
]

export const currencyDescriptions: Record<string, string> = {
  PLN: 'The Polish Zloty is the official currency of Poland. It is issued by the National Bank of Poland.',
  JPY: 'The Japanese Yen is the official currency of Japan. It is the third most traded currency in the foreign exchange market.',
  CAD: 'The Canadian Dollar is the official currency of Canada. It is often referred to as the loonie.',
  USD: 'The US Dollar is the official currency of the United States of America.',
  RUB: 'The Russian Ruble is the official currency of the Russian Federation.',
}
