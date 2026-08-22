import type { Currency } from '../../types/currency';
import { CurrencyInput } from '../CurrencyInput/CurrencyInput';
import { FilterButtons } from '../FilterButtons/FilterButtons';
import styles from './Converter.module.scss';

type ConverterProps = {
  currencies: Currency[];
  from: Currency;
  to: Currency;
  amount: string;
  rate: number;
  date: string;
  time: string;
};

export const Converter = ({ currencies, from, to, amount, rate, date, time }: ConverterProps) => {
  const convertedAmount = (Number(amount) * rate).toFixed(2);

  return (
    <div className={styles.container}>
      <p className={styles.subtitle}>
        {amount} {from.title} is
      </p>
      <p className={styles.rate}>
        {convertedAmount} {to.title}
      </p>
      <p className={styles.datetime}>
        {date} {time}
      </p>

      <CurrencyInput
        amountLabel="Сколько отдаёте"
        currencyLabel="Валюта, которую отдаёте"
        amount={amount}
        currencyCode={from.code}
        currencies={currencies}
      />
      <CurrencyInput
        amountLabel="Сколько получаете"
        currencyLabel="Валюта, которую получаете"
        amount={convertedAmount}
        currencyCode={to.code}
        currencies={currencies}
      />

      <FilterButtons />
    </div>
  );
};
