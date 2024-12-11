using System.Text.Json.Serialization;

namespace API.Crud.Flights.Models;

[JsonSerializable(typeof(FlightBooking))]
[JsonSerializable(typeof(IEnumerable<FlightBooking>))]
[JsonSerializable(typeof(FlightBookingDto))]
public partial class JsonContext : JsonSerializerContext
{
}
