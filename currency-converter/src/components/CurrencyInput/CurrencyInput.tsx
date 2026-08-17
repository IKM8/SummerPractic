import type { Currency } from '../../types/currency'
import styles from './CurrencyInput.module.scss'

type CurrencyInputProps = {
  amount: string
  currencyCode: string
  currencies: Currency[]
}

export const CurrencyInput = ({ amount, currencyCode, currencies }: CurrencyInputProps) => {
  return (
    <div className={styles.field}>
      <input
        type="text"
        inputMode="decimal"
        value={amount}
        readOnly
        className={styles.amount}
        aria-label="Amount"
      />
      <select value={currencyCode} disabled className={styles.currency} aria-label="Currency">
        {currencies.map((currency) => (
          <option value={currency.code} key={currency.code}>
            {currency.code}
          </option>
        ))}
      </select>
    </div>
  )
}
