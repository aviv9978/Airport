import { LegStatus } from './../models/legStatus';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ProcessLog } from '../models/ProcessLog';
import { HttpClient } from '@angular/common/http';
import { map, pipe } from 'rxjs';
import { LegStatusService } from './httpServices/leg-status.service';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  hubUrl = 'https://localhost:7297/terminalHub';
  baseUrl = 'https://localhost:7297/api/LegStatus';
  hubConnectionBuilder?: HubConnection;

  public hubLogs: ProcessLog[] = [];
  constructor(private legService: LegStatusService) {}

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
    this.hubConnectionBuilder?.on('logExitUpdate', (data) => {
      let obj = JSON.parse(data);
      this.updateLogExitDataListener(obj);
    });
  };

  public updateLegStatus = (legsStatus: LegStatus[]) => {
    this.hubConnectionBuilder?.on(
      'updateLegStatus',
      (legStatusServer: LegStatus) => {
        for (let legStatus of legsStatus) {
          if (legStatus.legNumber === legStatusServer.legNumber) {
            legStatus.isOccupied = legStatusServer.isOccupied;
            if (legStatus.isOccupied) legStatus.flight = legStatusServer.flight;
            else legStatus.flight = undefined;
            console.log(
              `legNum: ${
                legStatus.legNumber
              } changed from ${!legStatus.isOccupied} to ${
                legStatus.isOccupied
              }`
            );
            break;
          }
        }
      }
    );
  };

  public getLegsFromServer = () => {
    return this.legService.getStatusLegs();
  };
  private updateLogExitDataListener = (obj: any) => {
    for (let i = this.hubLogs.length - 1; i >= 0; i--) {
      if (this.hubLogs[i].id === obj.procLogID) {
        this.hubLogs[i].exitTime = obj.exitTime;
      }
      break;
    }
  };
}
