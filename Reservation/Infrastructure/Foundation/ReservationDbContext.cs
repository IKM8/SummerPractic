using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation;

public class ReservationDbContext( DbContextOptions<ReservationDbContext> options ) : DbContext( options )
{
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.ApplyConfigurationsFromAssembly( typeof( ReservationDbContext ).Assembly );
    }
}