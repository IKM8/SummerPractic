using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomTypeRepository
{
    IReadOnlyList<RoomType> GetAll();
    IReadOnlyList<RoomType> GetByProperty( Guid propertyId );
    RoomType? GetById( Guid id );
    void Add( RoomType roomType );
    void Update( RoomType roomType );
    void Delete( RoomType roomType );
}