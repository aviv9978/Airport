using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using ConsoleSimulator.Models;

HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7297/api/Flights/") };

System.Timers.Timer timer = new System.Timers.Timer(5000);
timer.Elapsed += async (s, e) => await CreateFlightAsync();
timer.Elapsed += async (s, e) => await CreateDepartureFlightAsync();

timer.Start();
//await CreateFlightAsync();
//await CreateFlightAsync();
Console.ReadLine(); var payload = new
{
    firstName = "John",
    lastName = "Doe"
};

// Deserialize the JSON string into a RequestBody object

async Task CreateFlightAsync()
{
    string flight = @"{
  ""Code"": ""9a9f888a-6b7c-4a5d-bd5b-17c5485a37f8"",
  ""Pilot"": {
    ""FirstName"": ""John"",
    ""LastName"": ""Doe"",
    ""Age"": 35,
    ""Rank"": 2
  },
  ""Plane"": {
    ""Company"": {
      ""Name"": ""Acme Airlines"",
      ""Country"": ""United States""
    },
    ""Model"": ""Boeing 747"",
    ""PassangerCount"": 400
  }
}";
    var requestBody = JsonSerializer.Deserialize<FlightDto>(flight);
    var response = await client.PostAsJsonAsync("AddDepartureFlight", requestBody);
    if (response.IsSuccessStatusCode)
     await Console.Out.WriteLineAsync(requestBody.Pilot.FirstName);
}
async Task CreateDepartureFlightAsync()
{
    string flight = @"{
  ""Code"": ""9a9f888a-6b7c-4a5d-bd5b-17c5485a37f8"",
  ""Pilot"": {
    ""FirstName"": ""John"",
    ""LastName"": ""Doe"",
    ""Age"": 35,
    ""Rank"": 2
  },
  ""Plane"": {
    ""Company"": {
      ""Name"": ""Acme Airlines"",
      ""Country"": ""United States""
    },
    ""Model"": ""Boeing 747"",
    ""PassangerCount"": 400
  }
}";
    var requestBody = JsonSerializer.Deserialize<FlightDto>(flight);
    var response = await client.PostAsJsonAsync("AddLandingFlight", requestBody);
     if (response.IsSuccessStatusCode)
     await Console.Out.WriteLineAsync(requestBody.Pilot.FirstName);
}