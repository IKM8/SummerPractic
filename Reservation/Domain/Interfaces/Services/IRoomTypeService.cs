using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IRoomTypeService
{
    IReadOnlyList<RoomType> GetRoomTypes( Guid propertyId );
    RoomType GetRoomType( Guid roomTypeId );
    Guid CreateRoomType(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount );
    void UpdateRoomType(
        Guid propertyId,
        Guid roomTypeId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount );
    void DeleteRoomType( Guid roomTypeId );
}