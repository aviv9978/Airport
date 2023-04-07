import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LogsBoardComponent } from './components/logs-board/logs-board.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot([{ path: 'logs-board', component: LogsBoardComponent }]),
  ],
})
export class PrivateRoutingModule {}
