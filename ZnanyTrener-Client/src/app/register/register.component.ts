import { Component, OnInit, ViewChild } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { UserToRegister } from '../_models/userToRegister';
import { AlertService } from '../_services/alert.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  @ViewChild('editForm', { static: true }) editForm: NgForm;
  registerForm: FormGroup;
  user: UserToRegister = {
    userName: "",
    firstName: "",
    lastName: "",
    description: "",
    specialization: "",
    city: "",
    phoneNumber: "",
    email: "",
    password: "",
    role: "",
    certificateNumber: "",
    certificateInstitution: "",
    certificateGainDate:  new Date(500000000000)
  };
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private alert: AlertService
  ) { }

  ngOnInit(): void {
    localStorage.removeItem('training_to_add');
    this.bsConfig = {
      containerClass: 'theme-green',
      isAnimated: true,
    };
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group(
      {
        userName: ['', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        description: [''],
        specialization: [''],
        city: [''],
        phoneNumber: [''],
        email: [''],
        certificateNumber: ['', Validators.pattern("[0-9]{2}/[0-9]{4}")],
        certificateInstitution: [''],
        certificateGainDate: [new Date(500000000000)],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
        role: ['User'],
      },
      { validator: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value
      ? null
      : { mismatch: true };
  }

  register() {
    if (this.registerForm.valid) {
      const user = Object.assign(this.user, this.registerForm.value);
      debugger;
      this.authService.register(user).subscribe((next) => {
        this.alert.success("Dziękujemy za rejestrację.");
      },
      error => {
        this.alert.error(error);
      })
    }
  }

}
