using Domain.Entities;

namespace WebApi.DTOs;

public class SearchResultItemDto
{
    public Guid PropertyId { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public Guid RoomTypeId { get; set; }
    public string RoomTypeName { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal TotalForStay { get; set; }
    public int AvailableRooms { get; set; }

    public static SearchResultItemDto From( AvailableRoomType available )
    {
        return new SearchResultItemDto
        {
            PropertyId = available.PropertyId,
            PropertyName = available.PropertyName,
            City = available.City,
            RoomTypeId = available.RoomTypeId,
            RoomTypeName = available.RoomTypeName,
            DailyPrice = available.DailyPrice,
            Currency = available.Currency,
            TotalForStay = available.TotalForStay,
            AvailableRooms = available.AvailableRooms
        };
    }
}