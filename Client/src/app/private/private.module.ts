import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogsBoardComponent } from './components/logs-board/logs-board.component';
import { ButtonComponent } from './components/button/button.component';
import { PrivateRoutingModule } from './private-routing.module';



@NgModule({
  declarations: [
    LogsBoardComponent,
    ButtonComponent
  ],
  imports: [
    CommonModule,
    PrivateRoutingModule
  ]
})
export class PrivateModule { }
