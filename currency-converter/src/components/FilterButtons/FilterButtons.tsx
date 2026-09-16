import { Button } from '../Button/Button';
import styles from './FilterButtons.module.scss';

export const FilterButtons = () => {
  return (
    <div className={styles.container}>
      <Button variant="save" className={styles.button}>
        + SAVE FILTER
      </Button>
      <Button variant="clear" className={styles.button}>
        CLEAR FILTERS
      </Button>
    </div>
  );
};
