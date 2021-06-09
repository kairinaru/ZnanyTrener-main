import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { TrainingToAdd } from '../_models/trainingToAdd';
import { TrainingToReturn } from '../_models/trainingToReturn';

@Injectable({
  providedIn: 'root',
})
export class TrainingService {
  url = environment.apiUrl + 'training/';

  constructor(private http: HttpClient) {}

  add(trainingToAdd: TrainingToAdd) {
    return this.http.post(this.url, trainingToAdd);
  }

  delete(id: number) {
    return this.http.delete(this.url + id);
  }

  getSingle(id: number): Observable<TrainingToReturn> {
    return this.http
      .get<TrainingToReturn>(this.url + id, { observe: 'response' })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getForCoach(id: number): Observable<TrainingToReturn[]> {
    return this.http
      .get<TrainingToReturn[]>(this.url + 'coach/' + id, { observe: 'response' })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getForUser(id: number): Observable<TrainingToReturn[]> {
    return this.http
      .get<TrainingToReturn[]>(this.url + 'user/' + id, { observe: 'response' })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }
}
