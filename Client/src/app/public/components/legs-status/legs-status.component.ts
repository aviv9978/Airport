import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProcessLog } from 'src/app/shared/models/ProcessLog';
import { LegStatus } from 'src/app/shared/models/legStatus';
import { LegStatusService } from 'src/app/shared/services/httpServices/leg-status.service';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'legs-status',
  templateUrl: './legs-status.component.html',
  styleUrls: ['./legs-status.component.scss'],
})
export class LegsStatusComponent implements OnInit, OnDestroy {
  legsStatus: LegStatus[] = [];
  private legsSubscription!: Subscription;

  constructor(
    private signalrService: SignalRService,
    private legStatusService: LegStatusService
  ) {}

  ngOnInit(): void {
    this.signalrService.startConnection();
    this.legsSubscription = this.legStatusService
      .getStatusLegs()
      .subscribe((res: LegStatus[]) => {
        this.legsStatus = res;
        console.log(this.legsStatus);
        this.signalrService.updateLegStatus(this.legsStatus);
      });
  }
  ngOnDestroy(): void {
    this.legsSubscription.unsubscribe();
    this.signalrService.stop();
  }

  clicktho() {
    console.log(this.legsStatus);
  }
}
