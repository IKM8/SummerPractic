using Domain.Entities;

namespace WebApi.DTOs;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestPhoneNumber { get; set; } = string.Empty;
    public int GuestCount { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }

    public static ReservationDto From( Reservation reservation )
    {
        return new ReservationDto
        {
            Id = reservation.Id,
            PropertyId = reservation.PropertyId,
            RoomTypeId = reservation.RoomTypeId,
            ArrivalDate = reservation.ArrivalDate,
            DepartureDate = reservation.DepartureDate,
            ArrivalTime = reservation.ArrivalTime,
            DepartureTime = reservation.DepartureTime,
            GuestName = reservation.GuestName,
            GuestPhoneNumber = reservation.GuestPhoneNumber,
            GuestCount = reservation.GuestCount,
            Total = reservation.Total,
            Currency = reservation.Currency,
            IsCancelled = reservation.IsCancelled
        };
    }
}