using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class PropertiesController( IPropertyService propertyService, IRoomTypeService roomTypeService ) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PropertyDto>> GetAll()
    {
        IReadOnlyList<Property> properties = propertyService.GetAllProperties();
        return Ok( properties.Select( PropertyDto.From ).ToList() );
    }

    [HttpGet( "{id}" )]
    public ActionResult<PropertyDto> GetById( Guid id )
    {
        Property property = propertyService.GetProperty( id );
        return Ok( PropertyDto.From( property ) );
    }

    [HttpPost]
    public ActionResult<PropertyDto> Create( CreatePropertyRequest request )
    {
        Guid id = propertyService.CreateProperty(
            request.Name,
            request.Country,
            request.City,
            request.Address,
            request.Latitude,
            request.Longitude );

        Property property = propertyService.GetProperty( id );
        return CreatedAtAction( nameof( GetById ), new { id }, PropertyDto.From( property ) );
    }

    [HttpPut( "{id}" )]
    public IActionResult Update( Guid id, CreatePropertyRequest request )
    {
        propertyService.UpdateProperty(
            id,
            request.Name,
            request.Country,
            request.City,
            request.Address,
            request.Latitude,
            request.Longitude );

        return NoContent();
    }

    [HttpDelete( "{id}" )]
    public IActionResult Delete( Guid id )
    {
        propertyService.DeleteProperty( id );
        return NoContent();
    }

    [HttpGet( "{propertyId}/roomtypes" )]
    public ActionResult<List<RoomTypeDto>> GetRoomTypes( Guid propertyId )
    {
        IReadOnlyList<RoomType> roomTypes = roomTypeService.GetRoomTypes( propertyId );
        return Ok( roomTypes.Select( RoomTypeDto.From ).ToList() );
    }

    [HttpPost( "{propertyId}/roomtypes" )]
    public ActionResult<RoomTypeDto> CreateRoomType( Guid propertyId, CreateRoomTypeRequest request )
    {
        Guid id = roomTypeService.CreateRoomType(
            propertyId,
            request.Name,
            request.DailyPrice,
            request.Currency,
            request.MinPersonCount,
            request.MaxPersonCount,
            request.AvailableRoomsCount );

        RoomType roomType = roomTypeService.GetRoomType( id );
        return CreatedAtAction( nameof( GetRoomTypes ), new { propertyId }, RoomTypeDto.From( roomType ) );
    }

    [HttpPut( "{propertyId}/roomtypes/{roomTypeId}" )]
    public IActionResult UpdateRoomType( Guid propertyId, Guid roomTypeId, CreateRoomTypeRequest request )
    {
        roomTypeService.UpdateRoomType(
            roomTypeId,
            request.Name,
            request.DailyPrice,
            request.Currency,
            request.MinPersonCount,
            request.MaxPersonCount,
            request.AvailableRoomsCount );

        return NoContent();
    }

    [HttpDelete( "{propertyId}/roomtypes/{roomTypeId}" )]
    public IActionResult DeleteRoomType( Guid propertyId, Guid roomTypeId )
    {
        roomTypeService.DeleteRoomType( roomTypeId );
        return NoContent();
    }
}