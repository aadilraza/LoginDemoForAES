import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'app/_services';


declare var require: any;
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-page2',
   templateUrl:'./login.html',
   styleUrls: ['./style.css']
})
export class loginComponent implements OnInit  { 
  
  name = 'Page2'; 
  private LOGO = require("./images/logo.png");
  model: any = {};
  loading = false;
  error = '';

  constructor(
      private router: Router,
      private http: Http,
        public userService: AuthenticationService) { }

    @HostListener('keydown', ['$event'])
    onKeyDown(event: any) {
        if (event.keyCode == 13)
        {
            
            $('.btn-primary').trigger('click');
        }
    }

  ngOnInit() {
      // reset login status
      this.userService.logout();
  }

  login() {
    this.loading = true;
      this.userService.login(this.model.username, this.model.password)
          .subscribe(result => {
            if (result === true) 
            {
                this.router.navigate(['/']);
            } 
            else 
            {
                this.error = 'Username or password is incorrect';
                this.loading = false;
                console.log(this.error);
            }
          },
          error => {
              this.loading = false;
              if (error.status === 401) {
                  this.error = 'Username or password is incorrect';
              }
              else if (error.status === 403) {
                  this.error = 'User already logged-in';
              }
              else {
                  this.error = 'Error authenticating user';
              }
        });
     
  }
}
