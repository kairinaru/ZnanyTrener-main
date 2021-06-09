import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '../_services/alert.service';

@Component({
  selector: 'app-payment-fail',
  templateUrl: './payment-fail.component.html',
  styleUrls: ['./payment-fail.component.css'],
})
export class PaymentFailComponent implements OnInit {
  constructor(private alert: AlertService, private router: Router) {}

  ngOnInit(): void {
    const tr = JSON.parse(localStorage.getItem('training_to_add'));
    localStorage.removeItem('training_to_add');
    this.alert.warning('Trening nie został dodany.');
    this.router.navigate(['/user/' + tr.coachId]);
  }
}
