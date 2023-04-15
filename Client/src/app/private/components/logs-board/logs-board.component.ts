import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProcessLog } from 'src/app/shared/models/ProcessLog';
import { SignalRService } from 'src/app/shared/services/signalR.service';
import { SortArrayService } from '../../../shared/services/sort-array.service';
import { Flight } from 'src/app/shared/models/Flight';
import { Company } from 'src/app/shared/models/forFlight/Company';
import { Pilot } from 'src/app/shared/models/forFlight/Pilot';
import { Plane } from 'src/app/shared/models/forFlight/Plane';

@Component({
  selector: 'logs-board',
  templateUrl: './logs-board.component.html',
  styleUrls: ['./logs-board.component.scss'],
})
export class LogsBoardComponent implements OnInit, OnDestroy {
  procLogs: ProcessLog[] = [];
  sortOrder = 'asc';

  constructor(public signalrService: SignalRService) {}

  async ngOnInit() {
    this.signalrService.startConnection();
    await this.signalrService.addLogsDataListener();
    this.procLogs = this.signalrService.procLogs;
    console.log(this.procLogs);
    console.log('555');
    console.log(this.procLogs);
  }

  ngOnDestroy(): void {
    this.signalrService.stop();
  }
  procLogSort(property: string) {
    console.log(this.procLogs);
    this.procLogs.sort((a, b) => {
      const aValue = a[property as keyof ProcessLog];
      const bValue = b[property as keyof ProcessLog];
      return this.basicSortLogic(aValue, bValue);
    });
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  flightSort(property: string) {
    console.log(this.procLogs);

    this.procLogs.sort((a, b) => {
      const aValue = this.getProperty(a.flight, property as keyof Flight);
      const bValue = this.getProperty(b.flight, property as keyof Flight);
      return this.basicSortLogic(aValue, bValue);
    });
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  companySort(property: string) {
    this.procLogs.sort((a, b) => {
      const aValue = this.getProperty(
        a.flight.plane.company,
        property as keyof Company
      );
      const bValue = this.getProperty(
        b.flight.plane.company,
        property as keyof Company
      );
      return this.basicSortLogic(aValue, bValue);
    });
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  pilotSort(property: string) {
    this.procLogs.sort((a, b) => {
      const aValue = this.getProperty(a.flight.pilot, property as keyof Pilot);
      const bValue = this.getProperty(b.flight.pilot, property as keyof Pilot);
      return this.basicSortLogic(aValue, bValue);
    });
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  planeSort(property: string) {
    this.procLogs.sort((a, b) => {
      const aValue = this.getProperty(a.flight.plane, property as keyof Plane);
      const bValue = this.getProperty(b.flight.plane, property as keyof Plane);
      return this.basicSortLogic(aValue, bValue);
    });
    this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
  }

  private basicSortLogic(aValue: any, bValue: any) {
    if (aValue < bValue) return this.sortOrder === 'asc' ? -1 : 1;
    if (aValue > bValue) return this.sortOrder === 'asc' ? 1 : -1;
    return 0;
  }
  private getProperty<T, K extends keyof T>(obj: T, key: K) {
    return obj[key];
  }
}
