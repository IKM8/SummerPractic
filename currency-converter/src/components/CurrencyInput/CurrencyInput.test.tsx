import { describe, expect, it } from 'vitest'
import { render, screen } from '@testing-library/react'
import { CurrencyInput } from './CurrencyInput'
import { currencies } from '../../data/currencies'

describe('CurrencyInput', () => {
  it('renders the amount in the input', () => {
    render(<CurrencyInput amount="100" currencyCode="PLN" currencies={currencies} />)

    expect(screen.getByDisplayValue('100')).toBeInTheDocument()
  })

  it('renders all currency options', () => {
    render(<CurrencyInput amount="100" currencyCode="PLN" currencies={currencies} />)

    expect(screen.getAllByRole('option')).toHaveLength(currencies.length)
    for (const currency of currencies) {
      expect(screen.getByRole('option', { name: currency.code })).toBeInTheDocument()
    }
  })

  it('selects the provided currency code', () => {
    render(<CurrencyInput amount="100" currencyCode="RUB" currencies={currencies} />)

    const select = screen.getByRole('combobox')
    expect(select).toHaveValue('RUB')
  })

  it('marks the input as read-only and disables the select', () => {
    render(<CurrencyInput amount="100" currencyCode="PLN" currencies={currencies} />)

    expect(screen.getByRole('textbox')).toHaveAttribute('readonly')
    expect(screen.getByRole('combobox')).toBeDisabled()
  })
})


//сверстать без интерактива