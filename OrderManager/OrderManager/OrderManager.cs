
void RequestData()
{
    Console.WriteLine( "Внимание, производится оформление заказа" );
    Console.WriteLine( "Введите название товара" );
    string? product = Console.ReadLine();
    Console.WriteLine( "Введите количество товара" );
    string? amountStr = Console.ReadLine();
    int amount = int.TryParse( amountStr, out int parsedAmount ) ? parsedAmount : 0;
    Console.WriteLine( "Введите ваше имя" );
    string? name = Console.ReadLine();
    Console.WriteLine( "Введите адрес доставки" );
    string? address = Console.ReadLine();
    string result = ConfirmData( product ?? "", amount, name ?? "", address ?? "" );
    Console.WriteLine( result );
}
string ConfirmData( string product, int amount, string name, string address )
{
    Console.WriteLine( $"Здравствуйте, {name}, вы заказали {amount} {product} на адрес {address}, все верно?" );
    string? answer = Console.ReadLine();
    switch ( ( answer ?? "" ).ToLower() )
    {
        case "yes":
            DateTime deliveryDate = DateTime.Now.AddDays( 3 );
            string result = $"{name}! Ваш заказ {product} в количестве {amount} оформлен! Ожидайте доставку по адресу {address} к {deliveryDate:dd.MM.yyyy}";
            return result;
        case "no":
            return "Оформление заказа отменено";
        default:
            return "Неправильный ввод. Оформление заказа отменено";
    }
}

RequestData();
