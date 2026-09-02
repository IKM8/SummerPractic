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

        Validators.ValidatePrice( dailyPrice );
        Validators.ValidatePersonCount( minPersonCount, maxPersonCount );
        Validators.ValidateAvailableRooms( availableRoomsCount );

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

        Validators.ValidatePrice( dailyPrice );
        Validators.ValidatePersonCount( minPersonCount, maxPersonCount );
        Validators.ValidateAvailableRooms( availableRoomsCount );
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
}