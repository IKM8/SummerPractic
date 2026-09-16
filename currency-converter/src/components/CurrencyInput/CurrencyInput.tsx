import type { Currency } from '../../types/currency';
import styles from './CurrencyInput.module.scss';

type CurrencyInputProps = {
  amountLabel: string;
  currencyLabel: string;
  amount: string;
  currencyCode: string;
  currencies: Currency[];
};

export const CurrencyInput = ({ amountLabel, currencyLabel, amount, currencyCode, currencies }: CurrencyInputProps) => {
  return (
    <div className={styles.field}>
      <input
        type="text"
        inputMode="decimal"
        aria-label={amountLabel}
        value={amount}
        readOnly
        className={styles.amount}
      />
      <select aria-label={currencyLabel} value={currencyCode} disabled className={styles.currency}>
        {currencies.map((currency) => (
          <option value={currency.code} key={currency.code}>
            {currency.code}
          </option>
        ))}
      </select>
    </div>
  );
};
