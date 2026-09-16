import type { ReactNode } from 'react';
import styles from './Button.module.scss';

type ButtonProps = {
  variant?: 'default' | 'active' | 'save' | 'clear';
  className?: string;
  children: ReactNode;
};

export const Button = ({ variant = 'default', className, children }: ButtonProps) => {
  return (
    <button type="button" className={`${styles.button} ${styles[variant]} ${className ?? ''}`}>
      {children}
    </button>
  );
};
