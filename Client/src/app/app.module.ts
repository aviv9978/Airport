import { RouterModule } from '@angular/router';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PublicModule } from './public/public.module';
import { PrivateModule } from './private/private.module';
import { SharedModule } from './shared/shared.module';
import { SignalRService } from './shared/services/signalR.service';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    PublicModule,
    PrivateModule,
    SharedModule,
    RouterModule,
  ],
  // providers: [
  //   SignalRService,
  //   {
  //     provide: APP_INITIALIZER,
  //     useFactory: (signalrService: SignalRService) => () =>
  //       signalrService.initiateSignalrConnection(),
  //     deps: [SignalRService],
  //     multi: true,
  //   },
  // ],
  bootstrap: [AppComponent],
})
export class AppModule {}
