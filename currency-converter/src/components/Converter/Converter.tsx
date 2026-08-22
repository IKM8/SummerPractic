import type { Currency } from '../../types/currency'
import { CurrencyInput } from '../CurrencyInput/CurrencyInput'
import heroImage from '../../assets/hero.png'
import styles from './Converter.module.scss'

type ConverterProps = {
  currencies: Currency[]
  from: Currency
  to: Currency
  amount: string
  rate: number
}

export const Converter = ({ currencies, from, to, amount, rate }: ConverterProps) => {
  const convertedAmount = (Number(amount) * rate).toFixed(2)

  return (
    <div className={styles.card}>
      <p className={styles.subtitle}>1 {from.title} is</p>
      <div className={styles.rate}>
        {rate} {to.title}
      </div>

      <div className={styles.inputs}>
        <CurrencyInput amount={amount} currencyCode={from.code} currencies={currencies} />
        <CurrencyInput amount={convertedAmount} currencyCode={to.code} currencies={currencies} />
      </div>

      <img src={heroImage} alt="Exchange rate chart" className={styles.chart} />
    </div>
  )
}
