using API.Crud.Flights.Models;
using API.Crud.Flights.Parses;
using API.Crud.Flights.Services;
using API.Crud.Flights.Settings;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<FlightBookingService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    { Title = "Flight API", Version = "v1" });
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.TypeInfoResolver = JsonContext.Default;
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["regex"] = typeof(RegexRouteConstraint);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight API v1"));
}

app.UseHttpsRedirection();

app.MapPost("/flightbookings", async (FlightBookingService service, FlightBookingDto dto) =>
{
    var booking = FlightBookingFactory.CreateFlightBooking(dto);
    await service.CreateAsync(booking);
    return Results.Created($"/flightbookings/{booking.Id}", booking);
}).WithName("CreateFlight");

app.MapGet("/flightbookings", async (FlightBookingService service) =>
{
    var bookings = await service.GetAsync();
    return Results.Ok(bookings);
}).WithName("GetFlights");

app.MapGet("/flightbookings/{id}", async (FlightBookingService service, string id) =>
{
    var booking = await service.GetAsync(id);
    if (booking is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(booking);
}).WithName("GetFlightById");

app.MapPut("/flightbookings/{id}", async (FlightBookingService service, string id, FlightBookingDto updatedBookingDto) =>
{
    var booking = await service.GetAsync(id);

    if (booking is null)
    {
        return Results.NotFound();
    }
    await service.UpdateAsync(id, updatedBookingDto);

    return Results.NoContent();
}).WithName("UpdateFlights");

app.MapDelete("/flightbookings/{id}", async (FlightBookingService service, string id) =>
{
    var booking = await service.GetAsync(id);
    if (booking is null) { return Results.NotFound(); }
    await service.RemoveAsync(id); return Results.NoContent();
}).WithName("DeleteFlight");

app.Run();