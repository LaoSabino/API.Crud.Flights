using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Crud.Flights.Models;

public class FlightBooking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    public required string PassengerName { get; set; }
    public required string FlightNumber { get; set; }
    public DateTime Date { get; set; }
}
