import { formatDate } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateFormatPipe',
})
export class DateFormatPipePipe implements PipeTransform {
  transform(value?: string | Date) {
    if (!value) return '';
    return formatDate(value, 'dd/MM/yy h:mm:ss a', 'en-US');
  }
}
