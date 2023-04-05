import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PublicRoutingModule } from './public-routing.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    PublicRoutingModule
  ],
  exports: []
})
export class PublicModule { }
