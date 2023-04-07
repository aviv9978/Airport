import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot([{ path: '', component: HomeComponent }]),
  ],
  exports: [RouterModule],
})
export class PublicRoutingModule {}
