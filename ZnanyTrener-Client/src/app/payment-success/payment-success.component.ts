import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '../_services/alert.service';
import { TrainingService } from '../_services/training.service';

@Component({
  selector: 'app-payment-success',
  templateUrl: './payment-success.component.html',
  styleUrls: ['./payment-success.component.css']
})
export class PaymentSuccessComponent implements OnInit {

  constructor(private trainingService: TrainingService, private alert: AlertService, private router: Router) { }

  ngOnInit(): void {
    this.addTraining();
  }

  addTraining() {
    const trainingToAdd = JSON.parse(localStorage.getItem('training_to_add'));

    this.trainingService.add(trainingToAdd).subscribe(
      (data) => {
        //this.deletePlanned();
        //this.modalRef.hide();
        localStorage.removeItem('training_to_add');
        this.alert.success('Trening został dodany, dziękujemy za umówienie wizyty.');
        this.router.navigate(['/user/' +  trainingToAdd.coachId]);

      },
      (error) => {
        console.log(error);
      }
    );
  }

}
