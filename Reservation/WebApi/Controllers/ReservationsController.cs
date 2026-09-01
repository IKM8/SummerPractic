using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReservationsController( IReservationService reservationService ) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<ReservationDto>> GetAll(
        [FromQuery] Guid? propertyId,
        [FromQuery] DateOnly? fromDate,
        [FromQuery] DateOnly? toDate,
        [FromQuery] string? guestName )
    {
        IReadOnlyList<Reservation> reservations = reservationService.GetReservations( propertyId, fromDate, toDate, guestName );
        return Ok( reservations.Select( ReservationDto.From ).ToList() );
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