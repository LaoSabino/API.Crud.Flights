using API.Crud.Flights.Models;
using API.Crud.Flights.Parses;
using API.Crud.Flights.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Crud.Flights.Services;

public class FlightBookingService
{
    private readonly IMongoCollection<FlightBooking> _flightBookings;

    public FlightBookingService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _flightBookings = database.GetCollection<FlightBooking>(mongoDbSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<FlightBooking>> GetAsync() =>
        await _flightBookings.Find(flightBooking => true).ToListAsync();

    public async Task<FlightBooking> GetAsync(string id) =>
        await _flightBookings.Find<FlightBooking>(flightBooking => flightBooking.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(FlightBooking newFlightBooking) =>
        await _flightBookings.InsertOneAsync(newFlightBooking);

    public async Task UpdateAsync(string id, FlightBookingDto updatedFlightBookingDto)
    {
        FlightBooking updatedFlightBooking = FlightBookingFactory.ParseFlightBooking(id, updatedFlightBookingDto);
        await _flightBookings.ReplaceOneAsync(flightBooking => flightBooking.Id == id, updatedFlightBooking);
    }

    public async Task RemoveAsync(string id) =>
        await _flightBookings.DeleteOneAsync(flightBooking => flightBooking.Id == id);
}
