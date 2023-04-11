import { Injectable } from '@angular/core';
import { ProcessLog } from '../models/ProcessLog';
import { Flight } from '../models/Flight';
import { Company } from '../models/forFlight/Company';
import { Plane } from '../models/forFlight/Plane';
import { Pilot } from '../models/forFlight/Pilot';

@Injectable({
  providedIn: 'root',
})
export class SortArrayService {
  constructor() {}

  // sortArray<T>(array: T[], property: keyof T | string): T[] {
  //   if (typeof property === 'string')
  //     return array.sort((a, b) =>
  //       this.getProperty(a, property).localeCompare(
  //         this.getProperty(b, property)
  //       )
  //     );
  //   else return array.sort((a, b) => a[property].localeCompare(b[property]));
  // }

  // getProperty<T>(obj: T, key: string): any {
  //   if (key.includes('.')) {
  //     const parts = key.split('.');
  //     const nestedKey = parts.shift()!;
  //     return this.getProperty(obj[nestedKey], parts.join('.'));
  //   }
  //   return obj[key];
  // }
}
