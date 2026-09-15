using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface IPropertyService
{
    IReadOnlyList<Property> GetAllProperties();
    Property GetProperty( Guid id );
    Guid CreateProperty( string name, string country, string city, string address, double latitude, double longitude );
    void UpdateProperty( Guid id, string name, string country, string city, string address, double latitude, double longitude );
    void DeleteProperty( Guid id );
}