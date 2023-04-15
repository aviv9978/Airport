import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PublicRoutingModule } from './public-routing.module';
import { LegsStatusComponent } from './components/legs-status/legs-status.component';



@NgModule({
  declarations: [
    LegsStatusComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    RouterModule 
  ],
  exports: []
})
export class PublicModule { }
