import styles from './MoreAbout.module.scss'
import { currencyDescriptions } from '../../data/currencies'
import type { Currency } from '../../types/currency'

type MoreAboutProps = {
  from: Currency
  to: Currency
}

export const MoreAbout = ({ from, to }: MoreAboutProps) => {
  const pair = [from, to]

  return (
    <section className={styles.section}>
      {pair.map((currency) => (
        <div key={currency.code} className={styles.item}>
          <h2 className={styles.title}>{currency.title}</h2>
          <p className={styles.description}>
            {currencyDescriptions[currency.code] ?? 'No description available.'}
          </p>
        </div>
      ))}
    </section>
  )
}
