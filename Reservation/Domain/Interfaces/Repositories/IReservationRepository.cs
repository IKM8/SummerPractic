using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    IReadOnlyList<Reservation> GetAll();
    IReadOnlyList<Reservation> GetFiltered( Guid? propertyId, DateOnly? fromDate, DateOnly? toDate, string? guestName );
    IReadOnlyList<Reservation> GetOverlapping( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate );
    Reservation? GetById( Guid id );
    void Add( Reservation reservation );
    void Update( Reservation reservation );
    void Delete( Reservation reservation );
}