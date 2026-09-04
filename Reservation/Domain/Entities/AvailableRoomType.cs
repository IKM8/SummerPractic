namespace Domain.Entities;

public class AvailableRoomType
{
    public Guid PropertyId { get; init; }
    public string PropertyName { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public Guid RoomTypeId { get; init; }
    public string RoomTypeName { get; init; } = string.Empty;
    public decimal DailyPrice { get; init; }
    public string Currency { get; init; } = string.Empty;
    public decimal TotalForStay { get; init; }
    public int AvailableRooms { get; init; }
}