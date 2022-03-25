import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Constants } from './constants';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  login(Email: string, password: string) {
    return this.http.post<any>(`${Constants.URL_BASE}/api/Login/Authenticate`, { Email, password })
      .pipe(map(user => {
        // login successful if there's a user in the response
        if (user) {
          // store user details and basic auth credentials in local storage 
          // to keep user logged in between page refreshes
          user.authdata = window.btoa(Email + ':' + password);
          sessionStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    sessionStorage.removeItem('currentUser');
  }
}
