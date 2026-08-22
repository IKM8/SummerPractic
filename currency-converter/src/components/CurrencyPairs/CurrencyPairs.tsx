import type { CurrencyPair } from '../../types/currencyPair';
import { Button } from '../Button/Button';
import styles from './CurrencyPairs.module.scss';

type CurrencyPairsProps = {
  pairs: CurrencyPair[];
  activePair: string;
};

export const CurrencyPairs = ({ pairs, activePair }: CurrencyPairsProps) => {
  return (
    <div className={styles.container}>
      {pairs.map((pair) => {
        const code = `${pair.from}/${pair.to}`;

        return (
          <Button key={code} variant={code === activePair ? 'active' : 'default'} className={styles.pair}>
            {code}
          </Button>
        );
      })}
    </div>
  );
};
