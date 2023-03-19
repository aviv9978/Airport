// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;

using ConsoleSimulator.Models;
using System.Net.Http.Json;

HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7275") };

System.Timers.Timer timer = new System.Timers.Timer(5000);
timer.Elapsed += (s, e) => CreateFlight();
timer.Start();

Console.ReadLine();

async void CreateFlight() => await client.PostAsJsonAsync("api/Flights", new FlightDto());
