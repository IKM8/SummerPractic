using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IReservationService
{
    IReadOnlyList<Reservation> GetReservations( Guid? propertyId, DateOnly? fromDate, DateOnly? toDate, string? guestName );
    Reservation GetReservation( Guid id );
    Guid CreateReservation(
        Guid roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime,
        string guestName,
        string guestPhoneNumber,
        int guestCount );
    void CancelReservation( Guid id );
    IReadOnlyList<AvailableRoomType> SearchAvailable( string? city, DateOnly arrivalDate, DateOnly departureDate, int guests, decimal? maxPrice );
}