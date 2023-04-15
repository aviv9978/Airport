import { Injectable } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LogsService {
  private logsUrl = `${environment.baseApi}/LegStatus`;

  constructor(private http: HttpClient) { }
}
