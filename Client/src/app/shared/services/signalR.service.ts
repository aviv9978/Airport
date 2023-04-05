import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Log } from '../models/Log';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  hubUrl = 'https://localhost:7297/terminalHub';
  hubConnection?: signalR.HubConnection;

  public hubLogs?: Log[];

  constructor() {}

  public startConnection() {
    try {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(this.hubUrl, {})
        .build();

      this.hubConnection.start().then(() => {
        console.log(
          `SignalR connection success! connectionId: ${this.hubConnection?.connectionId}`
        );
      });
    } catch (error) {
      console.log(`SignalR connection error: ${error}`);
    }
    this.hubConnection?.onclose((error) => {
      console.error(`Connection closed: ${error}`);
    });
  }

  public addLogsDataListener = () => {
    this.hubConnection?.on('GetLogs', (log) => {
      this.hubLogs = log;
      console.log(log);
    });
  };
}
