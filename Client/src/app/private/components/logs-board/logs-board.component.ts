import { Component, OnInit } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';
import { ProcessLog } from 'src/app/shared/models/ProcessLog';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'logs-board',
  templateUrl: './logs-board.component.html',
  styleUrls: ['./logs-board.component.scss'],
})
export class LogsBoardComponent implements OnInit {
  logs: ProcessLog[] = [];
  private hubConnectionBuilder!: HubConnection;
  constructor(public signalrService: SignalRService) {}

  ngOnInit(): void {
    this.signalrService.startConnection();
    this.signalrService.addLogsDataListener();
  }
}
