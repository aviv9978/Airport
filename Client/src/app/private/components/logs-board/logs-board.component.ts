import { Component, OnInit } from '@angular/core';
import { Log } from 'src/app/shared/models/Log';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'logs-board',
  templateUrl: './logs-board.component.html',
  styleUrls: ['./logs-board.component.scss'],
})
export class LogsBoardComponent implements OnInit {
  logs: Log[] = [];

  constructor(
    public signalrService: SignalRService,
    private http: HttpClient
  ) {}
  ngOnInit(): void {
    this.signalrService.startConnection();
    this.signalrService.addLogsDataListener();
  }
}
