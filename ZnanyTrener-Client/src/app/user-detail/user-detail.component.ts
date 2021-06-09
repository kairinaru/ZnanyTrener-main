import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CertificateToAdd } from '../_models/certificateToAdd';
import { UserDetail } from '../_models/userDetail';
import { CertificateService } from '../_services/certificate.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css'],
})
export class UserDetailComponent implements OnInit {
  user: UserDetail;
  logged: UserDetail;
  certificates: CertificateToAdd[];
  isCollapsed: boolean = false;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    localStorage.removeItem('training_to_add');
    this.route.data.subscribe((data) => {
      debugger;
      this.user = data['user'];
      this.logged = JSON.parse(localStorage.getItem('user'));
    });
  }

  isMe(): boolean {
    return this.logged.id === this.user.id;
  }

  isCoach(): boolean {
    return this.user.role === 'Coach';
  }
}
