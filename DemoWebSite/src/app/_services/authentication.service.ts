import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { RequestOptions } from '@angular/http';

@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(username: string, password: string) {
        debugger;
        let apiUrl = "http://localhost:63808/api";
       // let User = JSON.stringify({ Username: username, Password: password });
        return this.http.post<any>(`${apiUrl}/Login`, { Username: username, Password: password })
            .pipe(map(user => {
                debugger;
                // login successful if there's a jwt token in the response
                    if (user) {
                    // if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
                //return user;
                return true;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}