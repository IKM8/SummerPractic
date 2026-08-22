import { describe, expect, it } from 'vitest';
import { render, screen } from '@testing-library/react';
import { MoreAbout } from './MoreAbout';
import { currencies } from '../../data/currencies';

const pln = currencies.find((c) => c.code === 'PLN')!;
const jpy = currencies.find((c) => c.code === 'JPY')!;

describe('MoreAbout', () => {
  it('renders the titles of both currencies', () => {
    render(<MoreAbout from={pln} to={jpy} />);

    expect(screen.getByText('Polish zloty')).toBeInTheDocument();
    expect(screen.getByText('Japanese yen')).toBeInTheDocument();
  });

  it('shows the description for the Polish zloty', () => {
    render(<MoreAbout from={pln} to={jpy} />);

    expect(screen.getByText(/The Polish zloty is the official currency/)).toBeInTheDocument();
  });

  it('shows the description for the Japanese yen', () => {
    render(<MoreAbout from={pln} to={jpy} />);

    expect(screen.getByText(/The Japanese yen is the official currency/)).toBeInTheDocument();
  });
});
