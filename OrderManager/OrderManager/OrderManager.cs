internal class Program
{
    private const int DeliveryDays = 3;

    private static void Main()
    {
        bool orderAccepted = false;

        while ( !orderAccepted )
        {
            string product = RequestString( "Введите название товара: " );
            int amount = RequestInt( "Введите количество товара: " );
            string name = RequestString( "Введите ваше имя: " );
            string address = RequestString( "Введите адрес доставки: " );

            bool isAgree = AskForConfirmation( name, amount, product, address );

            if ( isAgree )
            {
                PrintOrderDetails( name, amount, product, address );
                orderAccepted = true;
            }
            else
            {
                Console.WriteLine( "Заказ отменён. Попробуйте оформить заново." );
            }
        }
    }

    private static string RequestString( string message )
    {
        Console.Write( message );
        string? input = Console.ReadLine();

        while ( string.IsNullOrWhiteSpace( input ) )
        {
            Console.Write( "Некорректный ввод. " + message );
            input = Console.ReadLine();
        }

        return input;
    }

    private static int RequestInt( string message )
    {
        Console.Write( message );
        bool isParsed = int.TryParse( Console.ReadLine(), out int result );

        while ( !isParsed || result <= 0 )
        {
            Console.Write( "Введите целое число больше нуля. " + message );
            isParsed = int.TryParse( Console.ReadLine(), out result );
        }

        return result;
    }

    private static bool AskForConfirmation( string name, int amount, string product, string address )
    {
        HashSet<string> positiveAnswers = new() { "да", "yes", "y" };

        Console.WriteLine( $"Здравствуйте, {name}, вы заказали {amount} {product} на адрес {address}, всё верно?" );
        string? answer = Console.ReadLine();

        while ( string.IsNullOrWhiteSpace( answer ) )
        {
            Console.Write( "Некорректный ввод. Введите да или нет: " );
            answer = Console.ReadLine();
        }

        return positiveAnswers.Contains( answer.ToLower() );
    }

    private static void PrintOrderDetails( string name, int amount, string product, string address )
    {
        DateTime deliveryDate = DateTime.Now.AddDays( DeliveryDays );
        Console.WriteLine( $"{name}! Ваш заказ {product} в количестве {amount} оформлен!" );
        Console.WriteLine( $"Ожидайте доставку по адресу {address} к {deliveryDate:dd.MM.yyyy}" );
    }
}