using Domain.Exceptions;

namespace Infrastructure.Foundation.Services;

public static class Validators
{
    public static void ValidatePrice( decimal dailyPrice )
    {
        if ( dailyPrice < 0 )
        {
            throw new BusinessRuleViolationException( "Цена за ночь не может быть отрицательной" );
        }
    }

    public static void ValidatePersonCount( int minPersonCount, int maxPersonCount )
    {
        if ( minPersonCount < 0 )
        {
            throw new BusinessRuleViolationException( "Минимальное количество гостей не может быть отрицательным" );
        }

        if ( maxPersonCount < 0 )
        {
            throw new BusinessRuleViolationException( "Максимальное количество гостей не может быть отрицательным" );
        }

        if ( minPersonCount > maxPersonCount )
        {
            throw new BusinessRuleViolationException( "Минимальное количество гостей не может превышать максимальное" );
        }
    }

    public static void ValidateAvailableRooms( int availableRoomsCount )
    {
        if ( availableRoomsCount < 0 )
        {
            throw new BusinessRuleViolationException( "Количество доступных номеров не может быть отрицательным" );
        }
    }

    public static void ValidateStringLength( string value, string fieldName, int maxLength )
    {
        if ( value.Length > maxLength )
        {
            throw new BusinessRuleViolationException( $"{fieldName} не может превышать {maxLength} символов" );
        }
    }
}