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

    public IReadOnlyList<Property> GetPage( int skip, int take )
    {
        return db.Properties.AsNoTracking().Skip( skip ).Take( take ).ToList();
    }

    public int GetCount()
    {
        return db.Properties.AsNoTracking().Count();
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