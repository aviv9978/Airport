import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogsBoardComponent } from './components/logs-board/logs-board.component';
import { ButtonComponent } from './components/button/button.component';
import { PrivateRoutingModule } from './private-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    LogsBoardComponent,
    ButtonComponent
  ],
  imports: [
    CommonModule,
    PrivateRoutingModule,
    SharedModule
  ],
  exports: [LogsBoardComponent]
})
export class PrivateModule { }
