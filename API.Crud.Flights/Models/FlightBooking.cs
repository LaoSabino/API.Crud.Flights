using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Crud.Flights.Models;

public class FlightBooking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string PassengerName { get; set; }
    public string FlightNumber { get; set; }
    public DateTime Date { get; set; }
}
