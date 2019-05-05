import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppState } from 'app/app.service';
import { XTestService } from 'app/xtest.service';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'my-app',
  template: '<router-outlet></router-outlet>'
})
export class AppComponent {
  constructor()
   { }
 
}
