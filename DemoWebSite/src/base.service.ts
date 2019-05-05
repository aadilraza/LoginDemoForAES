import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'
import { AppState } from 'app/app.service'
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class BaseWebService {
    protected token: string;
    protected headers: HttpHeaders;
    protected http: HttpClient;
   
    constructor() {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;

        this.headers = new HttpHeaders();
        this.headers.set('Authorization', 'Bearer ' + this.token);
        this.headers.set('Content-Type', 'application/json');
    }



}