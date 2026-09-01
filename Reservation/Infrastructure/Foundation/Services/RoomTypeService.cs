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

        var roomType = new RoomType( propertyId, name, dailyPrice, currency, minPersonCount, maxPersonCount, availableRoomsCount );
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
        var roomType = GetRoomType( roomTypeId );
        roomType.Update( name, dailyPrice, currency, minPersonCount, maxPersonCount, availableRoomsCount );
        roomTypeRepository.Update( roomType );
    }

    public void DeleteRoomType( Guid roomTypeId )
    {
        var roomType = GetRoomType( roomTypeId );

        if ( reservationRepository.GetOverlapping( roomTypeId, DateOnly.MinValue, DateOnly.MaxValue ).Count > 0 )
        {
            throw new BusinessRuleViolationException( "Нельзя удалить тип номера, на котором есть бронирования" );
        }

        roomTypeRepository.Delete( roomType );
    }
}