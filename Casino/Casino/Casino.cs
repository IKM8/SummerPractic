
int balance = 1000;
int multiplicator = 1;
bool isRunning = true;

while ( isRunning )

{
    Console.WriteLine( "1 Считать баланс" );
    Console.WriteLine( "2 Сделать ставку" );
    Console.WriteLine( "3 Выход" );


    string? command = Console.ReadLine();
    if ( command is not null ) ParseCommand( command );
}

void ParseCommand( string command )
{
    switch ( command )
    {
        case "1":
            Console.WriteLine( $"Ваш баланс: {balance}$" );
            break;
        case "2":
            Play();
            break;
        case "3":
            Console.WriteLine( "Выход из игры." );
            isRunning = false;
            break;
        default:
            Console.WriteLine( "Неизвестная команда" );
            break;
    }
}

void Play()
{
    if ( balance <= 0 )
    {
        Console.WriteLine( "У вас закончились деньги" );
        return;
    }

    Console.WriteLine( $"Ваш баланс: {balance}$. Введите ставку:" );

    if ( !int.TryParse( Console.ReadLine(), out int stavka ) || stavka <= 0 )
    {
        Console.WriteLine( "Некорректная сумма ставки" );
        return;
    }

    if ( stavka > balance )
    {
        Console.WriteLine( "У вас нет столько денег для ставки!" );
        return;
    }

    int winnum = Random.Shared.Next( 1, 21 );
    int[] luckyNumbers = { 18, 19, 20 };

    if ( luckyNumbers.Contains( winnum ) )
    {
        int winAmount = stavka * ( 1 + ( multiplicator * winnum ) % 17 );
        balance += winAmount;
        Console.WriteLine( $"Выпало число {winnum}." );
        Console.WriteLine( $"Вы выиграли: {winAmount}" );
        Console.WriteLine( $"Ваш новый баланс: {balance}" );
    }
    else
    {
        balance -= stavka;
        Console.WriteLine( $"Проигрыш. Выпало число {winnum}." );
        Console.WriteLine( $"Вы потеряли: {stavka}" );
        Console.WriteLine( $"Ваш новый баланс: {balance}" );
    }
}
