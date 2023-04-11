import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateFormatPipePipe } from './pipes/date-format-pipe.pipe';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [DateFormatPipePipe],
  imports: [CommonModule, RouterModule],
  exports: [DateFormatPipePipe],
})
export class SharedModule {}
