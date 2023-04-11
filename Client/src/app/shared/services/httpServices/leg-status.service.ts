import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { LegStatus } from '../../models/legStatus';

@Injectable({
  providedIn: 'root',
})
export class LegStatusService {
  constructor(private http: HttpClient) {}

  getStatusLegs = () => {
    return this.http
      .get(`https://localhost:7297/api/LegStatus/GetLegStatus`)
      .pipe(map((res: any) => res.$values));
  };
}
