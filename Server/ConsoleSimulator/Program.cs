using System.Net.Http.Json;
using ConsoleSimulator.Models;

HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7297/api/Flights/") };

//System.Timers.Timer timer = new System.Timers.Timer(5000);
//timer.Elapsed += (s, e) => CreateFlight();
//timer.Start();

await CreateFlightAsync();
Console.ReadLine();

async Task CreateFlightAsync()
{
    var stam = new FlightDto { Name = "aaa" };
    var response = await client.PostAsJsonAsync("AddFlight", stam);
    if (response.IsSuccessStatusCode)
        await Console.Out.WriteLineAsync(stam.Name);
}