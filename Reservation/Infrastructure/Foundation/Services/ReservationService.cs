using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class ReservationService(
    IReservationRepository reservationRepository,
    IRoomTypeRepository roomTypeRepository,
    IPropertyRepository propertyRepository ) : IReservationService
{
    public IReadOnlyList<Reservation> GetReservations( Guid? propertyId, DateOnly? fromDate, DateOnly? toDate, string? guestName )
    {
        return reservationRepository.GetFiltered( propertyId, fromDate, toDate, guestName );
    }

    public Reservation GetReservation( Guid id )
    {
        return reservationRepository.GetById( id ) ?? throw new EntityNotFoundException( "Бронирование не найдено" );
    }

    public Guid CreateReservation(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber,
        int guestCount )
    {
        RoomType roomType = roomTypeRepository.GetById( roomTypeId )
            ?? throw new EntityNotFoundException( "Тип номера не найден" );

        if ( departureDate <= arrivalDate )
        {
            throw new BusinessRuleViolationException( "Дата выезда должна быть позже даты заезда" );
        }

        if ( string.IsNullOrWhiteSpace( guestName ) )
        {
            throw new BusinessRuleViolationException( "Имя гостя обязательно для заполнения" );
        }

        if ( string.IsNullOrWhiteSpace( guestPhoneNumber ) )
        {
            throw new BusinessRuleViolationException( "Номер телефона гостя обязателен для заполнения" );
        }

        if ( guestCount < roomType.MinPersonCount || guestCount > roomType.MaxPersonCount )
        {
            throw new BusinessRuleViolationException( $"Количество гостей должно быть от {roomType.MinPersonCount} до {roomType.MaxPersonCount}" );
        }

        int bookedRooms = CountBookedRooms( roomTypeId, arrivalDate, departureDate );
        if ( bookedRooms >= roomType.AvailableRoomsCount )
        {
            throw new BusinessRuleViolationException( "Нет свободных номеров на выбранный период" );
        }

        int nights = departureDate.DayNumber - arrivalDate.DayNumber;
        decimal total = roomType.DailyPrice * nights;

        Reservation reservation = new Reservation(
            roomType.PropertyId,
            roomTypeId,
            arrivalDate,
            departureDate,
            arrivalTime,
            departureTime,
            guestName,
            guestPhoneNumber,
            guestCount,
            total,
            roomType.Currency );

        reservationRepository.Add( reservation );
        return reservation.Id;
    }

    public void CancelReservation( Guid id )
    {
        Reservation reservation = GetReservation( id );

        if ( reservation.IsCancelled )
        {
            throw new BusinessRuleViolationException( "Бронирование уже отменено" );
        }

        reservation.Cancel();
        reservationRepository.Update( reservation );
    }

    public IReadOnlyList<AvailableRoomType> SearchAvailable( string? city, DateOnly arrivalDate, DateOnly departureDate, int guests, decimal? maxPrice )
    {
        if ( departureDate <= arrivalDate )
        {
            throw new BusinessRuleViolationException( "Дата выезда должна быть позже даты заезда" );
        }

        List<AvailableRoomType> results = new List<AvailableRoomType>();
        IReadOnlyList<Property> properties = propertyRepository.GetAll();
        IReadOnlyList<RoomType> allRoomTypes = roomTypeRepository.GetAll();

        Dictionary<Guid, List<RoomType>> roomTypesByProperty = new Dictionary<Guid, List<RoomType>>();

        foreach ( RoomType roomType in allRoomTypes )
        {
            if ( !roomTypesByProperty.TryGetValue( roomType.PropertyId, out List<RoomType>? list ) )
            {
                list = new List<RoomType>();
                roomTypesByProperty[roomType.PropertyId] = list;
            }

            list.Add( roomType );
        }

        int nights = arrivalDate.DayNumber - departureDate.DayNumber;

        foreach ( Property property in properties )
        {
            if ( !string.IsNullOrWhiteSpace( city ) && !property.City.Equals( city, StringComparison.OrdinalIgnoreCase ) )
            {
                continue;
            }

            if ( !roomTypesByProperty.TryGetValue( property.Id, out List<RoomType>? roomTypes ) )
            {
                continue;
            }

            foreach ( RoomType roomType in roomTypes )
            {
                if ( roomType.PropertyId != property.Id )
                {
                    continue;
                }

                if ( guests < roomType.MinPersonCount || guests > roomType.MaxPersonCount )
                {
                    continue;
                }

                if ( maxPrice.HasValue && roomType.DailyPrice > maxPrice.Value )
                {
                    continue;
                }

                int bookedRooms = CountBookedRooms( roomType.Id, arrivalDate, departureDate );
                int availableRooms = roomType.AvailableRoomsCount - bookedRooms;

                if ( availableRooms <= 0 )
                {
                    continue;
                }

                results.Add( new AvailableRoomType
                {
                    PropertyId = property.Id,
                    PropertyName = property.Name,
                    City = property.City,
                    RoomTypeId = roomType.Id,
                    RoomTypeName = roomType.Name,
                    DailyPrice = roomType.DailyPrice,
                    Currency = roomType.Currency,
                    TotalForStay = roomType.DailyPrice * nights,
                    AvailableRooms = availableRooms
                } );
            }
        }

        return results;
    }

    private int CountBookedRooms( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate )
    {
        return reservationRepository.GetOverlapping( roomTypeId, arrivalDate, departureDate ).Count;
    }
}