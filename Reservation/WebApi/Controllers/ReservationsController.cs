using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReservationsController( IReservationService reservationService ) : ControllerBase
{
    [HttpGet]
    public ActionResult<PaginatedResult<ReservationDto>> GetAll(
        [FromQuery] Guid? propertyId,
        [FromQuery] DateOnly? fromDate,
        [FromQuery] DateOnly? toDate,
        [FromQuery] string? guestName,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10 )
    {
        int skip = ( page - 1 ) * pageSize;
        int totalCount = reservationService.GetReservations( propertyId, fromDate, toDate, guestName ).Count;
        List<Reservation> reservations = reservationService.GetReservations( propertyId, fromDate, toDate, guestName ).Skip( skip ).Take( pageSize ).ToList();

        PaginatedResult<ReservationDto> result = new PaginatedResult<ReservationDto>
        {
            Items = reservations.Select( ReservationDto.From ).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return Ok( result );
    }

    [HttpGet( "{id}" )]
    public ActionResult<ReservationDto> GetById( Guid id )
    {
        Reservation reservation = reservationService.GetReservation( id );
        return Ok( ReservationDto.From( reservation ) );
    }

    [HttpPost]
    public ActionResult<ReservationDto> Create( CreateReservationRequest request )
    {
        Guid id = reservationService.CreateReservation(
            request.RoomTypeId,
            request.ArrivalDate,
            request.DepartureDate,
            request.ArrivalTime,
            request.DepartureTime,
            request.GuestName,
            request.GuestPhoneNumber,
            request.GuestCount );

        Reservation reservation = reservationService.GetReservation( id );
        return CreatedAtAction( nameof( GetById ), new { id }, ReservationDto.From( reservation ) );
    }

    [HttpDelete( "{id}" )]
    public IActionResult Cancel( Guid id )
    {
        reservationService.CancelReservation( id );
        return NoContent();
    }

    [HttpGet( "search" )]
    public ActionResult<List<SearchResultItemDto>> Search(
        [FromQuery] string? city,
        [FromQuery] DateOnly arrivalDate,
        [FromQuery] DateOnly departureDate,
        [FromQuery] int guests,
        [FromQuery] decimal? maxPrice )
    {
        IReadOnlyList<AvailableRoomType> results = reservationService.SearchAvailable( city, arrivalDate, departureDate, guests, maxPrice );
        return Ok( results.Select( SearchResultItemDto.From ).ToList() );
    }
}