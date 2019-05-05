import { routing, appRoutingProviders } from './app.routing';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppState } from 'app/app.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { HttpModule } from '@angular/http';
import { XTestService } from 'app/xtest.service';
import { page2Component } from 'app/Page2/page2.component';
import { Page1Component } from 'app/Page1/page1.component';
import { loginComponent } from './login/login.component';
import { AuthGuard } from './_guards';
import { AuthenticationService } from './_services';
import { HttpClientModule } from '@angular/common/http'; 


@NgModule({
  imports: [BrowserModule, FormsModule,HttpModule,routing,HttpClientModule],
  declarations: [AppComponent,Page1Component,page2Component,loginComponent],
  bootstrap: [AppComponent],
  providers: [AppState,XTestService,appRoutingProviders,AuthGuard,AuthenticationService]
})
export class AppModule {

  constructor(public appState: AppState) { }
}
