using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Xml.Linq;
using ConsoleSimulator.Models;

HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7297/api/Flights/") };
SemaphoreSlim semaphore = new SemaphoreSlim(4);

while (true)
{
    await semaphore.WaitAsync(3); // Wait until there are 3 available slots in the semaphore
    try
    {
        await SendApiRequests(); // Send both API requests
    }
    finally
    {
        semaphore.Release(3); // Release 3 semaphore slots when both API requests are done
    }
    await Task.Delay(1000); // Wait for 1 second before sending the next set of API requests
}

async Task CreateDepartureFlightAsync()
{
    var code = Guid.NewGuid();
    string flight = @"{
  ""Code"": """ + code + @""",
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
async Task CreateLandingFlightAsync()
{
    var code = Guid.NewGuid();
    string flight = @"{
  ""Code"": """ + code + @""",
  ""Pilot"": {
    ""FirstName"": ""John"",
    ""LastName"": ""Cena"",
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
async Task SendApiRequests()
{
    await Task.Delay(2000);
    var request1 = CreateDepartureFlightAsync();
    await Task.Delay(2000);
    var request2 = CreateLandingFlightAsync();
    await Task.Delay(500);
    var request3 = CreateDepartureFlightAsync();
    await Task.WhenAll(request1, request2, request3);
}