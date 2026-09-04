namespace WebApi.DTOs;

public class CreateRoomTypeRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int AvailableRoomsCount { get; set; }
    public string Services { get; set; } = string.Empty;
    public string Amenities { get; set; } = string.Empty;
}