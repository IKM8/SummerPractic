import { describe, expect, it } from 'vitest';
import { render, screen } from '@testing-library/react';
import { CurrencyPairs } from './CurrencyPairs';
import { currencyPairs } from '../../data/currencyPairs';

describe('CurrencyPairs', () => {
  it('renders every currency pair', () => {
    render(<CurrencyPairs pairs={currencyPairs} activePair="PLN/JPY" />);

    expect(screen.getByRole('button', { name: 'PLN/CAD' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'PLN/JPY' })).toBeInTheDocument();
  });
});
