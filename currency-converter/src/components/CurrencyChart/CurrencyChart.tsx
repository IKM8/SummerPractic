import chartImage from '../../assets/chart.png';
import { Button } from '../Button/Button';
import styles from './CurrencyChart.module.scss';

type CurrencyChartProps = {
  periods: number[];
  currentPeriod: number;
};

export const CurrencyChart = ({ periods, currentPeriod }: CurrencyChartProps) => {
  return (
    <div className={styles.container}>
      <div className={styles.periods}>
        {periods.map((period) => (
          <Button
            key={period}
            variant={period === currentPeriod ? 'active' : 'default'}
            className={styles['period-button']}
          >
            {period} MIN
          </Button>
        ))}
      </div>

      <img className={styles.chart} src={chartImage} alt="Exchange rate chart" />
    </div>
  );
};
