import { describe, expect, it } from 'vitest';
import { render, screen } from '@testing-library/react';
import { CurrencyInput } from './CurrencyInput';
import { currencies } from '../../data/currencies';

const renderCurrencyInput = (currencyCode: string) =>
  render(
    <CurrencyInput
      amountLabel="Сколько отдаёте"
      currencyLabel="Валюта, которую отдаёте"
      amount="100"
      currencyCode={currencyCode}
      currencies={currencies}
    />
  );

describe('CurrencyInput', () => {
  it('renders the amount in the input', () => {
    renderCurrencyInput('PLN');

    expect(screen.getByDisplayValue('100')).toBeInTheDocument();
  });

  it('renders all currency options', () => {
    renderCurrencyInput('PLN');

    expect(screen.getAllByRole('option')).toHaveLength(currencies.length);
    for (const currency of currencies) {
      expect(screen.getByRole('option', { name: currency.code })).toBeInTheDocument();
    }
  });

  it('selects the provided currency code', () => {
    renderCurrencyInput('EUR');

    const select = screen.getByRole('combobox');
    expect(select).toHaveValue('EUR');
  });

  it('marks the input as read-only and disables the select', () => {
    renderCurrencyInput('PLN');

    expect(screen.getByRole('textbox')).toHaveAttribute('readonly');
    expect(screen.getByRole('combobox')).toBeDisabled();
  });
});
