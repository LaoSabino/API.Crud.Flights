using System.Text.Json.Serialization;

namespace API.Crud.Flights.Models;

[JsonSerializable(typeof(FlightBooking))]
[JsonSerializable(typeof(IEnumerable<FlightBooking>))]
public partial class JsonContext : JsonSerializerContext
{
}
