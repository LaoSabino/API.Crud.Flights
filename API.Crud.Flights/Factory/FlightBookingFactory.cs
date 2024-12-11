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
}
