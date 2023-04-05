import { Component } from '@angular/core';
import { SignalRService } from 'src/app/shared/services/signalR.service';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent {

  constructor(public signalR: SignalRService) {
    
  }
}
