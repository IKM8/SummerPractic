using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class EFRoomTypeRepository( ReservationDbContext db ) : IRoomTypeRepository
{
    public IReadOnlyList<RoomType> GetAll()
    {
        return db.RoomTypes.AsNoTracking().Where( r => r.IsActive ).ToList();
    }

    public IReadOnlyList<RoomType> GetByProperty( Guid propertyId )
    {
        return db.RoomTypes.AsNoTracking().Where( r => r.PropertyId == propertyId && r.IsActive ).ToList();
    }

    public RoomType? GetById( Guid id )
    {
        return db.RoomTypes.Include( r => r.Property ).FirstOrDefault( r => r.Id == id );
    }

    public void Add( RoomType roomType )
    {
        db.RoomTypes.Add( roomType );
        db.SaveChanges();
    }

    public void Update( RoomType roomType )
    {
        db.RoomTypes.Update( roomType );
        db.SaveChanges();
    }

    public void Delete( RoomType roomType )
    {
        roomType.Deactivate();
        db.RoomTypes.Update( roomType );
        db.SaveChanges();
    }
}