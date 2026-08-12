using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    IReadOnlyList<Property> GetAll();
    Property? GetById( Guid id );
    void Add( Property property );
    void Update( Property property );
    void Delete( Property property );
}