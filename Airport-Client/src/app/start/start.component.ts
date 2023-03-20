import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css'],
})
export class StartComponent implements OnInit {
  flights: any[] | undefined;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const pilot: any = {
      name: 'aviv',
    };
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(pilot);
    this.http
      .post('https://localhost:7297/api/Pilots/AddPilot', body, {
        headers: headers,
      })
      .subscribe(
        (res) => console.log('HTTP response', res),
        (err) => console.log('HTTP Error', err),
        () => console.log('HTTP request completed.')
      );
    console.log(body);
  }
}
