import { Component, OnInit } from '@angular/core';
import { ProcessLog } from 'src/app/shared/models/ProcessLog';
import { LegStatus } from 'src/app/shared/models/legStatus';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'legs-status',
  templateUrl: './legs-status.component.html',
  styleUrls: ['./legs-status.component.scss'],
})
export class LegsStatusComponent implements OnInit {
  legsStatus: LegStatus[] = this.signalrService.legsStatus;
  constructor(private signalrService: SignalRService) {}

  ngOnInit(): void {
    this.signalrService.startConnection();
    this.signalrService.getLegsFromServer();
    this.signalrService.updateLegStatus();
  }

  clicktho() {
    this.signalrService.getLegsFromServer();
    console.log(this.signalrService.legsStatus);
  }
}
