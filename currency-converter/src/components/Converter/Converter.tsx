import type { Currency } from '../../types/currency'
import { CurrencyInput } from '../CurrencyInput/CurrencyInput'
import heroImage from '../../assets/hero.png'
import styles from './Converter.module.scss'

type ConverterProps = {
  currencies: Currency[]
  fromCode: string
  toCode: string
  amount: string
  rate: number
}

export const Converter = ({ currencies, fromCode, toCode, amount, rate }: ConverterProps) => {
  return (
    <div className={styles.card}>
      <p className={styles.subtitle}>1 Polish zloty is</p>
      <div className={styles.rate}>
        {rate} Japanese yen
      </div>

      <div className={styles.inputs}>
        <CurrencyInput amount={amount} currencyCode={fromCode} currencies={currencies} />
        <CurrencyInput amount={rate.toFixed(2)} currencyCode={toCode} currencies={currencies} />
      </div>

      <img src={heroImage} alt="Exchange rate chart" className={styles.chart} />
    </div>
  )
}
