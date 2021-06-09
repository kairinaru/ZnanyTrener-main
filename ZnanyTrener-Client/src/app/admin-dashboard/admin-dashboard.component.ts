import { Component, OnInit } from '@angular/core';
import { UserDetail } from '../_models/userDetail';
import { AlertService } from '../_services/alert.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent implements OnInit {
  users: UserDetail[];

  constructor(private alert: AlertService, private userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getAll().subscribe((data) => {
      this.users = data;
    });
  }

  deleteUser(user: UserDetail) {
    this.alert.confirm('Czy na pewno chcesz usunąć tego użytkownika?', () => {
      this.userService.deleteUser(user.id).subscribe(
        () => {
          this.alert.success('usunięto.');
          this.getUsers();
        },
        (error) => {
          this.alert.error('Wystąpił błąd');
        }
      );
    });
  }
}
