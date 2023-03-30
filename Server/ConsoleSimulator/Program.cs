using System.Net.Http.Json;
using ConsoleSimulator.Models;

HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7297/api/Flights/") };

//System.Timers.Timer timer = new System.Timers.Timer(5000);
//timer.Elapsed += async (s, e) => await CreateFlightAsync();
//timer.Start();
await CreateFlightAsync();
await CreateFlightAsync();
Console.ReadLine();

async Task CreateFlightAsync()
{
    var stam = new FlightDto { Name = "aaa" };
    var response = await client.PostAsJsonAsync("AddDepartureFlight", stam);
    if (response.IsSuccessStatusCode)
        await Console.Out.WriteLineAsync(stam.Name);
}