import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { throwIfEmpty } from 'rxjs/operators';
import { UserToLogin } from '../_models/userToLogin';
import { AlertService } from '../_services/alert.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  model: UserToLogin = { userName: '', password: '' };
  constructor(
    private authService: AuthService,
    private router: Router,
    private alert: AlertService
  ) {}

  ngOnInit(): void {
    localStorage.removeItem('training_to_add');
  }

  login() {
    this.authService.login(this.model).subscribe(
      (next) => {
        const role = localStorage.getItem('role');
        if(role==='User') {
          this.router.navigate(['/find-coach']);
        }
        else if(role==='Coach') {
          this.router.navigate(['/me']);
        }
        else if(role==='Admin') {
          this.router.navigate(['/admin']);
        }
        
      },
      (error) => {
        this.alert.error('Zła nazwa użytkownika lub hasło.');
      }
    );
  }
}
