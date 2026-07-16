
Console.WriteLine( "Введите математическое выражение" );
string? input = Console.ReadLine();
if ( string.IsNullOrEmpty( input ) )
{
    Console.WriteLine( "Ввод пуст" );
    return;
}
int pos = -1;
if ( input.Contains( '+' ) )
    pos = input.IndexOf( '+' );
else if ( input.Contains( '-' ) )
    pos = input.IndexOf( '-' );
else if ( input.Contains( '/' ) )
    pos = input.IndexOf( '/' );
else if ( input.Contains( '*' ) )
    pos = input.IndexOf( '*' );
if ( pos == -1 )
{
    Console.WriteLine( "Не найдена операция" );
    return;
}
int firstOperand = int.Parse( input.Substring( 0, pos ) );
int secondOperand = int.Parse( input.Substring( pos + 1 ) );
string operation = input[ pos ].ToString();


float calculate( float firstOperand, float secondOperand, string operation )
{
    switch ( operation )
    {
        case "+":
            return firstOperand + secondOperand;
        case "-":
            return firstOperand - secondOperand;
        case "*":
            return firstOperand * secondOperand;
        case "/":
            if ( secondOperand == 0 )
            {
                Console.WriteLine( "Ошибка деления на ноль" );
                return 0;
            }
            return ( float )firstOperand / secondOperand;
        default:
            Console.WriteLine( "Неправильная операция" );
            return 0;
    }
}

float result = calculate( firstOperand, secondOperand, operation );
Console.WriteLine( $"Результат {result}" );
