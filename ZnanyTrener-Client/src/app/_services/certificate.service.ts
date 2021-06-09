import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CertificateToAdd } from '../_models/certificateToAdd';

@Injectable({
  providedIn: 'root'
})
export class CertificateService {
  url = environment.apiUrl + 'certificate/'

  constructor(private http: HttpClient) {}

  add(certificateToAdd: CertificateToAdd) {
    return this.http.post(this.url, certificateToAdd);
  }

}
