import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LegStatusService {
  private legStatusUrl = `${environment.baseApi}/LegStatus`;
  
  constructor(private http: HttpClient) {}
  
  getStatusLegs = () => {
    return this.http
      .get(`${this.legStatusUrl}/GetLegStatus`)
      .pipe(map((res: any) => res));
  };
}
