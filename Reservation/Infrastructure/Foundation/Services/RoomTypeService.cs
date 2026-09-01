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
        int availableRoomsCount )
    {
        if ( propertyRepository.GetById( propertyId ) is null )
        {
            throw new EntityNotFoundException( "Объект размещения не найден" );
        }

        ValidatePrice( dailyPrice );
        ValidatePersonCount( minPersonCount, maxPersonCount );

        RoomType roomType = new RoomType( propertyId, name, dailyPrice, currency, minPersonCount, maxPersonCount, availableRoomsCount );
        roomTypeRepository.Add( roomType );

        return roomType.Id;
    }

    public void UpdateRoomType(
        Guid roomTypeId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount )
    {
        RoomType roomType = GetRoomType( roomTypeId );
        ValidatePrice( dailyPrice );
        ValidatePersonCount( minPersonCount, maxPersonCount );
        roomType.Update( name, dailyPrice, currency, minPersonCount, maxPersonCount, availableRoomsCount );
        roomTypeRepository.Update( roomType );
    }

    public void DeleteRoomType( Guid roomTypeId )
    {
        RoomType roomType = GetRoomType( roomTypeId );

        if ( reservationRepository.GetOverlapping( roomTypeId, DateOnly.MinValue, DateOnly.MaxValue ).Count > 0 )
        {
            throw new BusinessRuleViolationException( "Нельзя удалить тип номера, на котором есть бронирования" );
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
        if ( minPersonCount > maxPersonCount )
        {
            throw new BusinessRuleViolationException( "Минимальное количество гостей не может превышать максимальное" );
        }
    }
}