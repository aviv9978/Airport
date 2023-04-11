import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProcessLog } from 'src/app/shared/models/ProcessLog';
import { LegStatus } from 'src/app/shared/models/legStatus';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'legs-status',
  templateUrl: './legs-status.component.html',
  styleUrls: ['./legs-status.component.scss'],
})
export class LegsStatusComponent implements OnInit, OnDestroy {
  constructor(private signalrService: SignalRService) {}
  legsStatus!: LegStatus[];
  private legsSubscription!: Subscription;

  ngOnInit(): void {
    this.signalrService.startConnection();
    this.legsSubscription = this.signalrService
      .getLegsFromServer()
      .subscribe((res: LegStatus[]) => {
        this.legsStatus = res;
        this.signalrService.updateLegStatus(this.legsStatus);
      });
  }
  ngOnDestroy(): void {
    this.legsSubscription.unsubscribe();
  }

  clicktho() {
    console.log(this.legsStatus);
  }
}
