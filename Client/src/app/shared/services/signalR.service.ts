import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Log } from '../models/Log';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  hubUrl = 'https://localhost:7297/terminalHub';
  hubConnectionBuilder?: HubConnection;

  public hubLogs?: Log[];

  constructor() {}

  public startConnection() {
    this.hubConnectionBuilder = new HubConnectionBuilder()
      .withUrl(this.hubUrl)
      .build();
    this.hubConnectionBuilder
      .start()
      .then(() =>
        console.log(
          `SignalR connection success! connectionId: ${this.hubConnectionBuilder?.connectionId}`
        )
      )
      .catch((error) =>
        console.log(`Error while connect with server: ${error}`)
      );
    
    this.hubConnectionBuilder?.onclose((error) => {
      console.error(`Connection closed: ${error}`);
    });
  }

  public addLogsDataListener = () => {
    this.hubConnectionBuilder?.on('logUpdate', (log) => {
      console.log(log);
      this.hubLogs = log;
    });
  };
}
