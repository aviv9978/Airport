import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PublicRoutingModule } from './public-routing.module';
import { LegsStatusComponent } from './components/legs-status/legs-status.component';



@NgModule({
  declarations: [
    LegsStatusComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PublicRoutingModule
  ],
  exports: []
})
export class PublicModule { }
