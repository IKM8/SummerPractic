import { describe, expect, it } from 'vitest';
import { render, screen } from '@testing-library/react';
import { CurrencyChart } from './CurrencyChart';

describe('CurrencyChart', () => {
  it('renders a button for every period', () => {
    render(<CurrencyChart periods={[1, 2]} currentPeriod={1} />);

    expect(screen.getByRole('button', { name: '1 MIN' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: '2 MIN' })).toBeInTheDocument();
  });

  it('renders the chart image', () => {
    render(<CurrencyChart periods={[1]} currentPeriod={1} />);

    expect(screen.getByAltText('Exchange rate chart')).toBeInTheDocument();
  });
});
