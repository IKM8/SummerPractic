import { Button } from '../Button/Button';
import type { Currency } from '../../types/currency';
import styles from './MoreAbout.module.scss';

type MoreAboutProps = {
  from: Currency;
  to: Currency;
};

export const MoreAbout = ({ from, to }: MoreAboutProps) => {
  return (
    <section className={styles.section}>
      <div className={styles.header}>
        <Button className={styles['pair-button']}>
          {from.code}/{to.code}: about
          <span className={styles.arrow} />
        </Button>
        <span className={styles.line} />
      </div>

      <div className={styles.item}>
        <h2 className={styles.title}>
          {from.title} - {from.code} - {from.symbol}
        </h2>
        <p className={styles.description}>{from.description}</p>
      </div>

      <div className={styles.item}>
        <h2 className={styles.title}>
          {to.title} - {to.code} - {to.symbol}
        </h2>
        <p className={styles.description}>{to.description}</p>
      </div>
    </section>
  );
};
