import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LegsStatusComponent } from './components/legs-status/legs-status.component';
import { LogsBoardComponent } from '../private/components/logs-board/logs-board.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'legs-status', component: LegsStatusComponent },
      { path: 'logs-board', component: LogsBoardComponent },
    ]),
  ],
  exports: [RouterModule],
})
export class PublicRoutingModule {}
