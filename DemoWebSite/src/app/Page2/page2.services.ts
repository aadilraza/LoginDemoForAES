import { AppState } from 'app/app.service';
import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { config } from 'app/shared/smartadmin.config';
import { StringifyOptions } from 'querystring';

@Injectable()
export class Source_hold_Service extends AppState {
      
    Service(method: string,body:string) 
    {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url = config.BaseUrl + method;
        return this.http.post(url, body, options).map(res => res.json())
    }
    ServiceNew(method: string,body:string) 
    {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url = config.BaseUrl + "source_hold/" + method;
        return this.http.post(url, body, options).map(res => res.json())
    }
    Init(method: string, body: string) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url = config.BaseUrl + "source_hold/" + method;
        return this.http.post(url, body, options).map(res => res.json())
    }
 
}