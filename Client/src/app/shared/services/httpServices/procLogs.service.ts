import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProcLogsService {
  private procLogsUrl = `${environment.baseApi}/ProcessLogs`;

  constructor(private http: HttpClient) {}

  getAllProcessLogs = () => {
    return this.http
      .get(`${this.procLogsUrl}/GetAllProcessLogs`)
      .pipe(map((res: any) => res));
  };
}
