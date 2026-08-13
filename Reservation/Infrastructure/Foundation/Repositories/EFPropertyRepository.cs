using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class EFPropertyRepository( ReservationDbContext db ) : IPropertyRepository
{
    public IReadOnlyList<Property> GetAll()
    {
        return db.Properties.AsNoTracking().ToList();
    }

    public Property? GetById( Guid id )
    {
        return db.Properties.Include( p => p.RoomTypes ).FirstOrDefault( p => p.Id == id );
    }

    public void Add( Property property )
    {
        db.Properties.Add( property );
        db.SaveChanges();
    }

    public void Update( Property property )
    {
        db.Properties.Update( property );
        db.SaveChanges();
    }

    public void Delete( Property property )
    {
        db.Properties.Remove( property );
        db.SaveChanges();
    }
}