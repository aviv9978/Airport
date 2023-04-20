import { LegStatus } from './../models/legStatus';
import { Injectable, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ProcessLog } from '../models/ProcessLog';
import { Subscription, firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProcLogsService } from './httpServices/procLogs.service';

@Injectable({
  providedIn: 'root',
})
export class SignalRService implements OnDestroy {
  hubUrl = `${environment.baseHub}`;
  hubConnectionBuilder?: HubConnection;
  public legsStatus: LegStatus[] = [];
  private procLogsSubscription!: Subscription;

  public procLogs: ProcessLog[] = [];

  constructor(public procLogSerivce: ProcLogsService) {}
  ngOnDestroy(): void {
    this.procLogsSubscription.unsubscribe();
  }

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

  stop() {
    return this.hubConnectionBuilder?.stop();
  }

  public addLogsDataListener = async () => {
    let observable = this.procLogSerivce.getAllProcessLogs();
    this.procLogs = await firstValueFrom(observable);
    this.procLogs.sort(
      (a, b) =>
        new Date(b.enterTime).getTime() - new Date(a.enterTime).getTime()
    );
    this.hubConnectionBuilder?.on('addLog', (log: ProcessLog) => {
      console.log(log);
      console.log(this.procLogs);
      this.procLogs.unshift(log);
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
            break;
          }
        }
      }
    );
  };

  private updateLogExitDataListener = (obj: any) => {
    for (let i = 0; i <= this.procLogs.length; i++) {
      if (this.procLogs[i].id === obj.procLogID) {
        this.procLogs[i].exitTime = obj.exitTime;
      }
    }
  };
}
