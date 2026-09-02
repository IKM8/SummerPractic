using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class EFReservationRepository( ReservationDbContext db ) : IReservationRepository
{
    public IReadOnlyList<Reservation> GetAll()
    {
        return db.Reservations
            .Include( r => r.Property )
            .Include( r => r.RoomType )
            .AsNoTracking()
            .ToList();
    }

    public IReadOnlyList<Reservation> GetFiltered( Guid? propertyId, DateOnly? fromDate, DateOnly? toDate, string? guestName )
    {
        IQueryable<Reservation> query = db.Reservations
            .Include( r => r.Property )
            .Include( r => r.RoomType )
            .AsNoTracking();

        if ( propertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == propertyId.Value );
        }

        if ( fromDate.HasValue )
        {
            query = query.Where( r => r.ArrivalDate >= fromDate.Value );
        }

        if ( toDate.HasValue )
        {
            query = query.Where( r => r.DepartureDate <= toDate.Value );
        }

        if ( !string.IsNullOrWhiteSpace( guestName ) )
        {
            query = query.Where( r => r.GuestName.ToLower().Contains( guestName.ToLower() ) );
        }

        return query.ToList();
    }

    public IReadOnlyList<Reservation> GetOverlapping( Guid roomTypeId, DateOnly arrivalDate, DateOnly departureDate )
    {
        return db.Reservations
            .AsNoTracking()
            .Where( r => r.RoomTypeId == roomTypeId
                && !r.IsCancelled
                && r.ArrivalDate < departureDate
                && r.DepartureDate > arrivalDate )
            .ToList();
    }

    public Reservation? GetById( Guid id )
    {
        return db.Reservations
            .Include( r => r.Property )
            .Include( r => r.RoomType )
            .FirstOrDefault( r => r.Id == id );
    }

    public void Add( Reservation reservation )
    {
        db.Reservations.Add( reservation );
        db.SaveChanges();
    }

    public void Update( Reservation reservation )
    {
        db.Reservations.Update( reservation );
        db.SaveChanges();
    }

    public void Delete( Reservation reservation )
    {
        db.Reservations.Remove( reservation );
        db.SaveChanges();
    }
}