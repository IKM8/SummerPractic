namespace Fighters.ConsoleUi;

public class ConsoleInput( IConsole console )
{
    public string ReadName( string prompt )
    {
        while ( true )
        {
            console.WriteLine( prompt );
            string? name = console.ReadLine();

            if ( !string.IsNullOrWhiteSpace( name ) )
            {
                return name;
            }

            console.WriteLine( "Имя не может быть пустым. Попробуйте ещё раз." );
        }
    }

    public T ReadEnumOption<T>( string prompt, IReadOnlyList<string> options ) where T : struct, Enum
    {
        while ( true )
        {
            console.WriteLine( prompt );

            for ( int i = 0; i < options.Count; i++ )
            {
                console.WriteLine( $"  {i} - {options[ i ]}" );
            }

            string? input = console.ReadLine();

            if ( int.TryParse( input, out int index ) && index >= 0 && index < options.Count )
            {
                return ( T )Enum.ToObject( typeof( T ), index );
            }

            console.WriteLine( "Некорректный ввод. Попробуйте ещё раз." );
        }
    }
}