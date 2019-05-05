import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
declare var jquery: any;
declare var $: any;
export type InternalStateType = {
    [key: string]: any
};

@Injectable()
export class AppState {
    constructor(public http: Http) {}

    Service(method: string,body:any) 
    {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let url = "http://localhost:63808/api/" + method;
        return this.http.post(url, body, options).map(res => res.json())
    }



}
