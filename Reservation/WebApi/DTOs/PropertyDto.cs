using Domain.Entities;

namespace WebApi.DTOs;

public class PropertyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<RoomTypeDto> RoomTypes { get; set; } = new();

    public static PropertyDto From( Property property )
    {
        return new PropertyDto
        {
            Id = property.Id,
            Name = property.Name,
            Country = property.Country,
            City = property.City,
            Address = property.Address,
            Latitude = property.Latitude,
            Longitude = property.Longitude,
            RoomTypes = property.RoomTypes.Select( RoomTypeDto.From ).ToList()
        };
    }
}