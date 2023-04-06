import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Log } from 'src/app/shared/models/Log';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'logs-board',
  templateUrl: './logs-board.component.html',
  styleUrls: ['./logs-board.component.scss'],
})
export class LogsBoardComponent implements OnInit {
  logs: Log[] = [];
  private hubConnectionBuilder!: HubConnection;
  constructor(public signalrService: SignalRService) {}

  ngOnInit(): void {
    // this.hubConnectionBuilder = new HubConnectionBuilder()
    //   .withUrl('https://localhost:7297/terminalHub')
    //   .configureLogging(LogLevel.Information)
    //   .build();
    // this.hubConnectionBuilder
    //   .start()
    //   .then(() => console.log('Connection started.......!'))
    //   .catch((err) => console.log('Error while connect with server'));
    // this.hubConnectionBuilder.on('logUpdate', (flight: any) => {
    //   console.log(flight);
    // });
    this.signalrService.startConnection();
    this.signalrService.addLogsDataListener();
  }
}
