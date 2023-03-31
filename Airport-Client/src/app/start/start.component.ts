import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';
@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css'],
})
export class StartComponent implements OnInit {
  private hubConnectionBuilder!: HubConnection;
  flights: any[] = [];
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // const pilot: any = {
    //   name: 'aviv',
    // };
    // const headers = { 'content-type': 'application/json' };
    // const body = JSON.stringify(pilot);
    // this.http
    //   .post('https://localhost:7297/api/Pilots/AddPilot', body, {
    //     headers: headers,
    //   })
    //   .subscribe(
    //     (res) => console.log('HTTP response', res),
    //     (err) => console.log('HTTP Error', err),
    //     () => console.log('HTTP request completed.')
    //   );
    // console.log(body);

    this.hubConnectionBuilder = new HubConnectionBuilder()
      .withUrl('https://localhost:7297/flightHub')
      .configureLogging(LogLevel.Information)
      .build();
    this.hubConnectionBuilder
      .start()
      .then(() => console.log('Connection started.......!'))
      .catch((err) => console.log('Error while connect with server'));

    this.hubConnectionBuilder.on('Update', (flight: any) => {
      this.flights.push(flight);
      console.log(flight);
    });
  }
}
