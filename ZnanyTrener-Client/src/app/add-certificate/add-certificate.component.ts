import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { CertificateToAdd } from '../_models/certificateToAdd';
import { UserDetail } from '../_models/userDetail';
import { AlertService } from '../_services/alert.service';
import { CertificateService } from '../_services/certificate.service';

@Component({
  selector: 'app-add-certificate',
  templateUrl: './add-certificate.component.html',
  styleUrls: ['./add-certificate.component.css']
})
export class AddCertificateComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  certificateToAdd: CertificateToAdd = {
    userId: 0,
    institution: "",
    number: "",
    gainDate: null
  };

  constructor(private certificateService: CertificateService, private alert: AlertService) { }

  ngOnInit(): void {
    localStorage.removeItem('training_to_add');
    this.bsConfig = {
      containerClass: 'theme-green',
      isAnimated: true,
    };
  }

  addCertificate() {
    this.certificateToAdd.userId = JSON.parse(localStorage.getItem('user')).id;
    debugger;
    this.certificateService.add(this.certificateToAdd).subscribe((data) => {
      this.alert.success('Pomyślnie dodano certyfikat');
    },
    error => {
      this.alert.error('Wystąpił błąd');
    });
  }

}
