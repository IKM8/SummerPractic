internal class Casino
{
    private const int Multiplicator = 1;
    private const int MinNumber = 1;
    private const int MaxNumber = 20;

    private static readonly int[] LuckyNumbers = { 18, 19, 20 };

    private int _balance;

    internal void Run()
    {
        bool isRunning = true;

        while ( isRunning )
        {
            PrintMenu();

            string? command = Console.ReadLine();
            isRunning = ParseCommand( command );
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine( "1 Считать баланс" );
        Console.WriteLine( "2 Сделать ставку" );
        Console.WriteLine( "3 Выход" );
    }

    private bool ParseCommand( string? command )
    {
        switch ( command )
        {
            case "1":
                Console.WriteLine( $"Ваш баланс: {_balance}$" );
                return true;
            case "2":
                Play();
                return true;
            case "3":
                Console.WriteLine( "Выход из игры." );
                return false;
            default:
                Console.WriteLine( "Неизвестная команда" );
                return true;
        }
    }

    private void Play()
    {
        if ( _balance <= 0 )
        {
            Console.WriteLine( "У вас закончились деньги" );
            return;
        }

        Console.WriteLine( $"Ваш баланс: {_balance}$. Введите ставку:" );

        if ( !int.TryParse( Console.ReadLine(), out int stavka ) )
        {
            Console.WriteLine( "Некорректная сумма ставки" );
            return;
        }

        if ( stavka <= 0 )
        {
            Console.WriteLine( "Ставка должна быть больше нуля" );
            return;
        }

        if ( stavka > _balance )
        {
            Console.WriteLine( "У вас нет столько денег для ставки!" );
            return;
        }

        MakeBet( stavka );
    }

    private void MakeBet( int stavka )
    {
        int winnum = Random.Shared.Next( MinNumber, MaxNumber + 1 );

        if ( LuckyNumbers.Contains( winnum ) )
        {
            int winAmount = stavka * ( 1 + Multiplicator * ( winnum % ( MinNumber - 1 ) ) );
            _balance += winAmount;
            Console.WriteLine( $"Выпало число {winnum}." );
            Console.WriteLine( $"Вы выиграли: {winAmount}" );
            Console.WriteLine( $"Ваш новый баланс: {_balance}" );
        }
        else
        {
            _balance -= stavka;
            Console.WriteLine( $"Проигрыш. Выпало число {winnum}." );
            Console.WriteLine( $"Вы потеряли: {stavka}" );
            Console.WriteLine( $"Ваш новый баланс: {_balance}" );
        }
    }
}