using Domain.Entities;

namespace WebApi.DTOs;

public class RoomTypeDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int AvailableRoomsCount { get; set; }

    public static RoomTypeDto From( RoomType roomType )
    {
        return new RoomTypeDto
        {
            Id = roomType.Id,
            PropertyId = roomType.PropertyId,
            Name = roomType.Name,
            DailyPrice = roomType.DailyPrice,
            Currency = roomType.Currency,
            MinPersonCount = roomType.MinPersonCount,
            MaxPersonCount = roomType.MaxPersonCount,
            AvailableRoomsCount = roomType.AvailableRoomsCount
        };
    }
}