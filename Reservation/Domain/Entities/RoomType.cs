namespace Domain.Entities;

public class RoomType
{
    public Guid Id { get; private set; }
    public Guid PropertyId { get; private set; }
    public string Name { get; private set; }
    public decimal DailyPrice { get; private set; }
    public string Currency { get; private set; }
    public int MinPersonCount { get; private set; }
    public int MaxPersonCount { get; private set; }
    public int AvailableRoomsCount { get; private set; }

    public Property? Property { get; private set; }

    private RoomType()
    {
        Name = string.Empty;
        Currency = string.Empty;
    }

    public RoomType(
        Guid propertyId,
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount )
    {
        Id = Guid.NewGuid();
        PropertyId = propertyId;
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        AvailableRoomsCount = availableRoomsCount;
    }

    public void Update(
        string name,
        decimal dailyPrice,
        string currency,
        int minPersonCount,
        int maxPersonCount,
        int availableRoomsCount )
    {
        Name = name;
        DailyPrice = dailyPrice;
        Currency = currency;
        MinPersonCount = minPersonCount;
        MaxPersonCount = maxPersonCount;
        AvailableRoomsCount = availableRoomsCount;
    }
}