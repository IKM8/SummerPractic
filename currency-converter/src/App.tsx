import { Converter } from './components/Converter/Converter';
import { CurrencyChart } from './components/CurrencyChart/CurrencyChart';
import { CurrencyPairs } from './components/CurrencyPairs/CurrencyPairs';
import { MoreAbout } from './components/MoreAbout/MoreAbout';
import { chartPeriods } from './data/chartPeriods';
import { currencies } from './data/currencies';
import { currencyPairs } from './data/currencyPairs';
import styles from './App.module.scss';

export const App = () => {
  const from = currencies.find((currency) => currency.code === 'PLN')!;
  const to = currencies.find((currency) => currency.code === 'JPY')!;
  const rate = 0.99;

  return (
    <main className={styles.page}>
      <div className={styles.topHalf}>
        <Converter
          currencies={currencies}
          amount="1"
          from={from}
          to={to}
          rate={rate}
          date="Fri, 05 Apr 2026"
          time="10:35 UTC"
        />
        <CurrencyChart periods={chartPeriods} currentPeriod={4} />
      </div>

      <div className={styles.bottomHalf}>
        <CurrencyPairs pairs={currencyPairs} activePair="PLN/JPY" />
        <MoreAbout from={from} to={to} />
      </div>
    </main>
  );
};
