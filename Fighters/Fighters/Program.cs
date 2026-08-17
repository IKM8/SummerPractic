using Fighters.Battle;
using Fighters.Models;

namespace Fighters;

public class Program
{
    private static readonly List<Character> _fighters = new();
    private static bool _isRunning = true;

    public static void Main()
    {
        while ( _isRunning )
        {
            Console.WriteLine( "\n-- Меню --" );
            Console.WriteLine( "1. Создать персонажа" );
            Console.WriteLine( "2. Показать всех персонажей" );
            Console.WriteLine( "3. Начать битву" );
            Console.WriteLine( "4. Выход" );

            string? input = Console.ReadLine();
            switch ( input )
            {
                case "1":
                    CreateCharacter();
                    break;
                case "2":
                    ShowCharacters();
                    break;
                case "3":
                    BattleEngine.Fight( _fighters );
                    break;
                case "4":
                    _isRunning = false;
                    Console.WriteLine( "Выход." );
                    break;
                default:
                    Console.WriteLine( "Неизвестная команда." );
                    break;
            }
        }
    }

    private static void CreateCharacter()
    {
        Console.Write( "Введите имя персонажа: " );
        string? name = Console.ReadLine();
        if ( string.IsNullOrWhiteSpace( name ) ) name = "Боец";

        Race race = ChooseEnum<Race>( "Выберите расу" );
        Weapon weapon = ChooseEnum<Weapon>( "Выберите оружие" );
        Armor armor = ChooseEnum<Armor>( "Выберите броню" );
        CharacterClass characterClass = ChooseEnum<CharacterClass>( "Выберите класс" );

        var character = new Character( name, race, weapon, armor, characterClass );
        _fighters.Add( character );
        Console.WriteLine( $"Персонаж {character.Name} создан!" );
    }

    private static T ChooseEnum<T>( string prompt ) where T : struct, Enum
    {
        while ( true )
        {
            Console.WriteLine( $"\n{prompt}:" );
            var values = Enum.GetValues<T>();
            foreach ( var value in values )
            {
                Console.WriteLine( $"  {( int )( object )value}. {value}" );
            }
            Console.Write( "Введите номер: " );

            if ( int.TryParse( Console.ReadLine(), out int choice ) && Enum.IsDefined( typeof( T ), choice ) )
            {
                return ( T )( object )choice;
            }
            Console.WriteLine( "Некорректный выбор, попробуйте снова." );
        }
    }

    private static void ShowCharacters()
    {
        if ( _fighters.Count == 0 )
        {
            Console.WriteLine( "Нет созданных персонажей." );
            return;
        }

        Console.WriteLine( "\n-- Персонажи --" );
        foreach ( var fighter in _fighters )
        {
            Console.WriteLine( fighter.ToString() );
        }
    }
}