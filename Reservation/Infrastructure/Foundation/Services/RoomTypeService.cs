using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class RoomTypeService(
    IRoomTypeRepository roomTypeRepository,
    IPropertyRepository propertyRepository,
    IReservationRepository reservationRepository ) : IRoomTypeService
{
    public IReadOnlyList<RoomType> GetRoomTypes( Guid propertyId )
    {
        return roomTypeRepository.GetByProperty( propertyId );
    }

    public RoomType GetRoomType( Guid roomTypeId )
    {
        return roomTypeRepository.GetById( roomTypeId ) ?? throw new EntityNotFoundException( "Тип номера не найден" );
    }

    public Guid CreateRoomType(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount,
        string services,
        string amenities )
    {
        if ( propertyRepository.GetById( propertyId ) is null )
        {
            throw new EntityNotFoundException( "Объект размещения не найден" );
        }

        ValidatePrice( dailyPrice );
        ValidatePersonCount( minPersonCount, maxPersonCount );
        ValidateAvailableRooms( availableRoomsCount );
        ValidateRequiredString( name, "Название" );
        ValidateStringLength( name, "Название", 100 );
        ValidateStringLength( services, "Сервисы", 500 );
        ValidateStringLength( amenities, "Удобства", 500 );

        RoomType roomType = new RoomType( propertyId, name, dailyPrice, currency, minPersonCount, maxPersonCount, availableRoomsCount, services, amenities );
        roomTypeRepository.Add( roomType );

        return roomType.Id;
    }

    public void UpdateRoomType(
        Guid propertyId,
        Guid roomTypeId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount,
        string services,
        string amenities )
    {
        RoomType roomType = GetRoomType( roomTypeId );

        if ( roomType.PropertyId != propertyId )
        {
            throw new BusinessRuleViolationException( "Тип номера не принадлежит указанному объекту размещения" );
        }

        ValidatePrice( dailyPrice );
        ValidatePersonCount( minPersonCount, maxPersonCount );
        ValidateAvailableRooms( availableRoomsCount );
        ValidateRequiredString( name, "Название" );
        ValidateStringLength( name, "Название", 100 );
        ValidateStringLength( services, "Сервисы", 500 );
        ValidateStringLength( amenities, "Удобства", 500 );
        roomType.Update( name, dailyPrice, currency, minPersonCount, maxPersonCount, availableRoomsCount, services, amenities );
        roomTypeRepository.Update( roomType );
    }

    public void DeleteRoomType( Guid propertyId, Guid roomTypeId )
    {
        RoomType roomType = GetRoomType( roomTypeId );

        if ( roomType.PropertyId != propertyId )
        {
            throw new BusinessRuleViolationException( "Тип номера не принадлежит указанному объекту размещения" );
        }

        roomTypeRepository.Delete( roomType );
    }

    private static void ValidatePrice( decimal dailyPrice )
    {
        if ( dailyPrice < 0 )
        {
            throw new BusinessRuleViolationException( "Цена за ночь не может быть отрицательной" );
        }
    }

    private static void ValidatePersonCount( int minPersonCount, int maxPersonCount )
    {
        if ( minPersonCount <= 0 )
        {
            throw new BusinessRuleViolationException( "Минимальное количество гостей не может быть меньше единицы" );
        }

        if ( maxPersonCount <= 0 )
        {
            throw new BusinessRuleViolationException( "Максимальное количество гостей не может быть меньше единицы" );
        }

        if ( minPersonCount > maxPersonCount )
        {
            throw new BusinessRuleViolationException( "Минимальное количество гостей не может превышать максимальное" );
        }
    }

    private static void ValidateAvailableRooms( int availableRoomsCount )
    {
        if ( availableRoomsCount < 0 )
        {
            throw new BusinessRuleViolationException( "Количество доступных номеров не может быть отрицательным" );
        }
    }

    private static void ValidateRequiredString( string value, string fieldName )
    {
        if ( string.IsNullOrWhiteSpace( value ) )
        {
            throw new BusinessRuleViolationException( $"{fieldName} обязательно для заполнения" );
        }
    }

    private static void ValidateStringLength( string value, string fieldName, int maxLength )
    {
        if ( value.Length > maxLength )
        {
            throw new BusinessRuleViolationException( $"{fieldName} не может превышать {maxLength} символов" );
        }
    }
}
