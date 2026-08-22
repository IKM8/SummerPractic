import { Converter } from './components/Converter/Converter'
import { MoreAbout } from './components/MoreAbout/MoreAbout'
import { currencies } from './data/currencies'
import styles from './App.module.scss'

export const App = () => {
  const from = currencies.find((c) => c.code === 'PLN')!
  const to = currencies.find((c) => c.code === 'JPY')!
  const rate = 0.99

  return (
    <main className={styles.page}>
      <Converter currencies={currencies} amount="1" from={from} to={to} rate={rate} />
      <MoreAbout from={from} to={to} />
    </main>
  )
}
