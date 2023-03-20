import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css']
})
export class StartComponent implements OnInit {
  flights: any[] | undefined;

  constructor(private http: HttpClient) 
  {

   }

  ngOnInit(): void {
    
    const flight: any = {
      name: 'aviv'
    };
    this.http.post('https://localhost:7297/api/Flights',).subscribe(
      flights => this.flights = flights,
      error => console.error(error)
    );
  }
}
