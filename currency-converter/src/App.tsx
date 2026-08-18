import { Converter } from './components/Converter/Converter'
import { MoreAbout } from './components/MoreAbout/MoreAbout'
import { currencies } from './data/currencies'
import styles from './App.module.scss'

function App() {
  const from = currencies.find((c) => c.code === 'PLN')!
  const to = currencies.find((c) => c.code === 'JPY')!
  const rate = 0.99

  return (
    <main className={styles.page}>
      <Converter currencies={currencies} amount="1" fromCode={from.code} toCode={to.code} rate={rate} />
      <MoreAbout from={from} to={to} />
    </main>
  )
}

export default App
