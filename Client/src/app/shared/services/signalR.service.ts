import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ProcessLog } from '../models/ProcessLog';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  hubUrl = 'https://localhost:7297/terminalHub';
  hubConnectionBuilder?: HubConnection;

  public hubLogs: ProcessLog[] = [];

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
    this.hubConnectionBuilder?.on('addLog', (log: ProcessLog) => {
      console.log(log);
      console.log(this.hubLogs);
      this.hubLogs?.push(log);
    });
    this.updateLogExitDataListener();
  };

  private updateLogExitDataListener = () => {
    this.hubConnectionBuilder?.on('logExitUpdate', (data) => {
      let obj = JSON.parse(data);

      for (let i = this.hubLogs.length - 1; i >= 0; i--) {
        if (this.hubLogs[i].id === obj.procLogID) {
          this.hubLogs[i].exitTime = obj.exitTime;
          break;
        }
      }
    });
  };
}
