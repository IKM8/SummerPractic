using Fighters.Battle;
using Fighters.Models;

namespace Fighters.ConsoleUi;

public class GameApp( IConsole console, ConsoleInput input, FightManager fightManager )
{
    private readonly List<IFighter> _fighters = new();
    private bool _isRunning = true;

    public void Run()
    {
        while ( _isRunning )
        {
            console.WriteLine( "\n-- Меню --" );
            console.WriteLine( "1. Создать персонажа" );
            console.WriteLine( "2. Показать всех персонажей" );
            console.WriteLine( "3. Начать бой" );
            console.WriteLine( "4. Выход" );

            string? command = console.ReadLine();

            switch ( command )
            {
                case "1":
                    CreateCharacter();
                    break;
                case "2":
                    ShowCharacters();
                    break;
                case "3":
                    StartFight();
                    break;
                case "4":
                    _isRunning = false;
                    console.WriteLine( "Выход." );
                    break;
                default:
                    console.WriteLine( "Некорректная команда." );
                    break;
            }
        }
    }

    private void CreateCharacter()
    {
        string name = input.ReadName( "Введите имя персонажа: " );
        Race race = input.ReadEnumOption<Race>( "Выберите расу:", Enum.GetNames<Race>() );
        Weapon weapon = input.ReadEnumOption<Weapon>( "Выберите оружие:", Enum.GetNames<Weapon>() );
        Armor armor = input.ReadEnumOption<Armor>( "Выберите броню:", Enum.GetNames<Armor>() );
        CharacterClass characterClass = input.ReadEnumOption<CharacterClass>( "Выберите класс:", Enum.GetNames<CharacterClass>() );

        Character character = new Character( name, race, weapon, armor, characterClass );
        _fighters.Add( character );
        console.WriteLine( $"Персонаж {character.Name} создан!" );
    }

    private void ShowCharacters()
    {
        if ( _fighters.Count == 0 )
        {
            console.WriteLine( "Список пуст." );
            return;
        }

        foreach ( IFighter fighter in _fighters )
        {
            console.WriteLine( fighter.ToString() ?? string.Empty );
        }
    }

    private void StartFight()
    {
        if ( _fighters.Count < 2 )
        {
            console.WriteLine( "Нужно минимум 2 бойца для битвы." );
            return;
        }

        FightReport report = fightManager.Fight( _fighters );

        foreach ( RoundReport round in report.Rounds )
        {
            console.WriteLine( $"\n-- Раунд {round.RoundNumber} --" );

            foreach ( AttackReport attack in round.Attacks )
            {
                if ( !attack.IsHit )
                {
                    console.WriteLine( $"{attack.AttackerName} промахнулся по {attack.DefenderName}" );
                }
                else
                {
                    console.WriteLine( $"{attack.AttackerName} нанёс {attack.Damage} урона {attack.DefenderName}" );
                }

                if ( attack.DefenderDied )
                {
                    console.WriteLine( $"{attack.DefenderName} пал в бою!" );
                }
            }
        }

        if ( report.Winner is not null )
        {
            console.WriteLine( $"\nПобедитель: {report.Winner.Name}" );
        }
        else
        {
            console.WriteLine( "\nБой не завершился победой." );
        }

        _fighters.Clear();
        console.WriteLine( "Арена очищена, можно набирать новых бойцов." );
    }
}