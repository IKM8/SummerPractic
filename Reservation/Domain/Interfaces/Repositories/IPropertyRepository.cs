using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    IReadOnlyList<Property> GetAll();
    IReadOnlyList<Property> GetPage( int skip, int take );
    int GetCount();
    Property? GetById( Guid id );
    void Add( Property property );
    void Update( Property property );
    void Delete( Property property );
}