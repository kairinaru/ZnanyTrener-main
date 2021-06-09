import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserDetail } from '../_models/userDetail';
import { UserToEdit } from '../_models/userToEdit';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url = environment.apiUrl + 'user/';

  constructor(private http: HttpClient) {}

  deleteUser(id: number) {
    return this.http.delete(this.url + id);
  }

  updateUser(userToEdit: UserToEdit) {
    return this.http.put(this.url, userToEdit);
  }

  getCoaches(keyWord: string): Observable<UserDetail[]> {
    return this.http
      .get<UserDetail[]>(this.url + '?keyWord=' + keyWord, {
        observe: 'response',
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getAll(): Observable<UserDetail[]> {
    return this.http
      .get<UserDetail[]>(this.url + 'all', {
        observe: 'response',
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getSingle(id: number): Observable<UserDetail> {
    return this.http
      .get<UserDetail>(this.url + id, {
        observe: 'response',
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
}
