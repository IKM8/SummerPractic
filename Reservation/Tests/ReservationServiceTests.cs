using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Infrastructure.Foundation.Services;
using Moq;

namespace Reservation.Tests;

public class ReservationServiceTests
{
    private readonly Mock<IReservationRepository> _reservationRepo = new();
    private readonly Mock<IRoomTypeRepository> _roomTypeRepo = new();
    private readonly Mock<IPropertyRepository> _propertyRepo = new();

    private ReservationService CreateService()
    {
        return new ReservationService( _reservationRepo.Object, _roomTypeRepo.Object, _propertyRepo.Object );
    }

    [Fact]
    public void CreateReservation_Throws_When_DepartureDateBeforeArrival()
    {
        var service = CreateService();

        var roomType = new RoomType( Guid.NewGuid(), "Test", 100m, "RUB", 1, 5, 10, "WiFi", "TV" );
        _roomTypeRepo.Setup( r => r.GetById( It.IsAny<Guid>() ) ).Returns( roomType );

        Assert.Throws<BusinessRuleViolationException>( () =>
            service.CreateReservation(
                Guid.NewGuid(),
                DateOnly.FromDateTime( DateTime.Today.AddDays( 5 ) ),
                DateOnly.FromDateTime( DateTime.Today ),
                new TimeOnly( 14, 0 ), new TimeOnly( 12, 0 ),
                "Guest", "+79991234567", 2 ) );
    }

    [Fact]
    public void CreateReservation_Throws_When_GuestNameEmpty()
    {
        var service = CreateService();

        var roomType = new RoomType( Guid.NewGuid(), "Test", 100m, "RUB", 1, 5, 10, "WiFi", "TV" );
        _roomTypeRepo.Setup( r => r.GetById( It.IsAny<Guid>() ) ).Returns( roomType );

        Assert.Throws<BusinessRuleViolationException>( () =>
            service.CreateReservation(
                Guid.NewGuid(),
                DateOnly.FromDateTime( DateTime.Today ),
                DateOnly.FromDateTime( DateTime.Today.AddDays( 2 ) ),
                new TimeOnly( 14, 0 ), new TimeOnly( 12, 0 ),
                "", "+79991234567", 2 ) );
    }

    [Fact]
    public void CreateReservation_Throws_When_GuestCountOutOfRange()
    {
        var service = CreateService();

        var roomType = new RoomType( Guid.NewGuid(), "Test", 100m, "RUB", 2, 5, 10, "WiFi", "TV" );
        _roomTypeRepo.Setup( r => r.GetById( It.IsAny<Guid>() ) ).Returns( roomType );

        Assert.Throws<BusinessRuleViolationException>( () =>
            service.CreateReservation(
                Guid.NewGuid(),
                DateOnly.FromDateTime( DateTime.Today ),
                DateOnly.FromDateTime( DateTime.Today.AddDays( 2 ) ),
                new TimeOnly( 14, 0 ), new TimeOnly( 12, 0 ),
                "Guest", "+79991234567", 1 ) );
    }

    [Fact]
    public void CreateReservation_Throws_When_AllRoomsBooked()
    {
        var service = CreateService();

        var roomType = new RoomType( Guid.NewGuid(), "Test", 100m, "RUB", 1, 5, 1, "WiFi", "TV" );
        _roomTypeRepo.Setup( r => r.GetById( It.IsAny<Guid>() ) ).Returns( roomType );

        var existingReservation = new Domain.Entities.Reservation(
            roomType.PropertyId, roomType.Id,
            DateOnly.FromDateTime( DateTime.Today ),
            DateOnly.FromDateTime( DateTime.Today.AddDays( 1 ) ),
            new TimeOnly( 14, 0 ), new TimeOnly( 12, 0 ),
            "Existing", "+79991234567", 2,
            100m, "RUB" );
        _reservationRepo.Setup( r => r.GetOverlapping( It.IsAny<Guid>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>() ) )
            .Returns( new List<Domain.Entities.Reservation> { existingReservation } );

        Assert.Throws<BusinessRuleViolationException>( () =>
            service.CreateReservation(
                Guid.NewGuid(),
                DateOnly.FromDateTime( DateTime.Today ),
                DateOnly.FromDateTime( DateTime.Today.AddDays( 2 ) ),
                new TimeOnly( 14, 0 ), new TimeOnly( 12, 0 ),
                "Guest", "+79991234567", 2 ) );
    }

    [Fact]
    public void CancelReservation_Throws_When_AlreadyCancelled()
    {
        var service = CreateService();

        var reservation = new Domain.Entities.Reservation(
            Guid.NewGuid(), Guid.NewGuid(),
            DateOnly.FromDateTime( DateTime.Today ),
            DateOnly.FromDateTime( DateTime.Today.AddDays( 1 ) ),
            new TimeOnly( 14, 0 ), new TimeOnly( 12, 0 ),
            "Guest", "+79991234567", 2,
            100m, "RUB" );
        reservation.Cancel();

        _reservationRepo.Setup( r => r.GetById( It.IsAny<Guid>() ) ).Returns( reservation );

        Assert.Throws<BusinessRuleViolationException>( () =>
            service.CancelReservation( reservation.Id ) );
    }
}
