import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { UserDetail } from "../_models/userDetail";
import { UserService } from "../_services/user.service";

@Injectable()
export class MyProfileResolver implements Resolve<UserDetail> {
  constructor(
    private userService: UserService,
    private router: Router,
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<UserDetail> {
    const user: UserDetail = JSON.parse(localStorage.getItem('user'));
    return this.userService.getSingle(user.id).pipe(
      catchError((error) => {
        console.error('Error');
        this.router.navigate(['/find-coach']);
        return of(null);
      })
    );
  }
}