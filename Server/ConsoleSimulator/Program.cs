using System.Net.Http.Json;
using ConsoleSimulator.Models;

HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7297") };

System.Timers.Timer timer = new System.Timers.Timer(5000);
timer.Elapsed += (s, e) => CreateFlight();
timer.Start();

Console.ReadLine();

async Task CreateFlight()
{
    var stam = new FlightDto { Name = "aaa" };
    var response = await client.PostAsJsonAsync("api/stams", stam);
    if (response.IsSuccessStatusCode)
        await Console.Out.WriteLineAsync(stam.Name);
}