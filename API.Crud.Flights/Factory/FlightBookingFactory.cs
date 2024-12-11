using API.Crud.Flights.Models;
using MongoDB.Bson;

namespace API.Crud.Flights.Parses;

public static class FlightBookingFactory
{
    public static FlightBooking CreateFlightBooking(FlightBookingDto dto)
    {
        return new FlightBooking
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Date = dto.Date,
            FlightNumber = dto.FlightNumber,
            PassengerName = dto.PassengerName
        };
    }

    public static FlightBooking ParseFlightBooking(string id, FlightBookingDto updatedFlightBookingDto) => new()
    {
        Date = updatedFlightBookingDto.Date,
        FlightNumber = updatedFlightBookingDto.FlightNumber,
        Id = id,
        PassengerName = updatedFlightBookingDto.PassengerName
    };
}
