import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDetail } from '../_models/userDetail';
import { UserToLogin } from '../_models/userToLogin';
import { UserToRegister } from '../_models/userToRegister';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  url = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: UserDetail;
  constructor(private http: HttpClient, private router: Router) {}

  register(userToRegister: UserToRegister) {
    return this.http.post(this.url + 'register', userToRegister);
  }

  login(userToLogin: UserToLogin)  {
    return this.http.post(this.url + 'login', userToLogin)
      .pipe(map((response: UserDetail) => {
        const user = response;
        if (user) {
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user;
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(this.currentUser));
          localStorage.setItem('role', this.decodedToken.role);
        }
      }))
  }

  isAdmin(): boolean {
    if(localStorage.getItem('role')==='Admin') {
      return true;
    }
    else {
      return false;
    }
  }

  isCoach(): boolean {
    if(localStorage.getItem('role')==='Coach') {
      return true;
    }
    else {
      return false;
    }
  }

  isUser(): boolean {
    if(localStorage.getItem('role')==='User') {
      return true;
    }
    else {
      return false;
    }
  }

  isSignedIn(): boolean {
    if(localStorage.getItem('token')) {
      return true;
    }
    else {
      return false;
    }
  }

  signOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('role');
    this.router.navigate(['/login']);
  }
}
