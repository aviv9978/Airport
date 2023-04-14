import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { LegStatus } from '../../models/legStatus';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class LegStatusService {
  constructor(private http: HttpClient) {}
  private legStatusUrl = `${environment.baseApi}/LegStatus`;

  getStatusLegs = () => {
    return this.http
      .get(`${this.legStatusUrl}/GetLegStatus`)
      .pipe(map((res: any) => res.$values));
  };
}
