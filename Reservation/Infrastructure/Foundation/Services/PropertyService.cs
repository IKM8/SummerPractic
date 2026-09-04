using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Foundation.Services;

public class PropertyService( IPropertyRepository propertyRepository ) : IPropertyService
{
    public IReadOnlyList<Property> GetAllProperties()
    {
        return propertyRepository.GetAll();
    }

    public Property GetProperty( Guid id )
    {
        return propertyRepository.GetById( id ) ?? throw new EntityNotFoundException( "Объект размещения не найден" );
    }

    public Guid CreateProperty( string name, string country, string city, string address, double latitude, double longitude )
    {
        ValidateStringLength( name, "Название", 100 );
        ValidateStringLength( country, "Страна", 100 );
        ValidateStringLength( city, "Город", 100 );
        ValidateStringLength( address, "Адрес", 200 );

        Property property = new Property( name, country, city, address, latitude, longitude );
        propertyRepository.Add( property );
        return property.Id;
    }

    public void UpdateProperty( Guid id, string name, string country, string city, string address, double latitude, double longitude )
    {
        ValidateStringLength( name, "Название", 100 );
        ValidateStringLength( country, "Страна", 100 );
        ValidateStringLength( city, "Город", 100 );
        ValidateStringLength( address, "Адрес", 200 );

        Property property = GetProperty( id );
        property.Update( name, country, city, address, latitude, longitude );
        propertyRepository.Update( property );
    }

    public void DeleteProperty( Guid id )
    {
        Property property = GetProperty( id );

        if ( property.RoomTypes.Count > 0 )
        {
            throw new BusinessRuleViolationException( "Нельзя удалить объект, у которого есть типы номеров" );
        }

        propertyRepository.Delete( property );
    }

    private static void ValidateStringLength( string value, string fieldName, int maxLength )
    {
        if ( value.Length > maxLength )
        {
            throw new BusinessRuleViolationException( $"{fieldName} не может превышать {maxLength} символов" );
        }
    }
}
