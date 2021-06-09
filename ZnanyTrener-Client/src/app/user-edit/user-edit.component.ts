import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { UserDetail } from '../_models/userDetail';
import { UserToEdit } from '../_models/userToEdit';
import { AlertService } from '../_services/alert.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
})
export class UserEditComponent implements OnInit {
  
  user: UserDetail;
  userEdit: UserToEdit;
  @ViewChild('editForm', { static: true }) editForm: NgForm;
  uploader: FileUploader;

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private alert: AlertService,
    private router: Router
  ) {}

  ngOnInit(): void {
    localStorage.removeItem('training_to_add');
    this.route.data.subscribe((data) => {
      this.user = data['user'];
    });
    this.initalizeUploader();
  }

  initalizeUploader() {
    this.uploader = new FileUploader({
      url: environment.apiUrl + 'user',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onCompleteAll = () => {
      this.alert.success('Pomyślnie zmieniono.');
      /*this.userService.getUser(this.authService.currentUser.id).subscribe(
        (user: User) => {
          this.photoUrl = user.photoUrl;
          this.authService.changeMemberPhoto(this.photoUrl);
        },
        (error) => {
          this.alertify.error(error);
        }
      );*/
      this.uploader.clearQueue();
    };
  }
  

  updateUser() {

    this.userEdit = {
      id: this.user.id,
      description: this.user.description,
      specialization: this.user.specialization,
      phoneNumber: this.user.phoneNumber,
      city: this.user.city,
      email: this.user.email
    };
    debugger;
    this.userService.updateUser(this.userEdit).subscribe((data) => {
      this.changePhoto();
      this.alert.success('Zaktualizowano');
      this.router.navigate(['/me']);
    },
    error => {
      this.alert.error('Błąd.');
    })

  }

  changePhoto() {
    if (this.uploader.getNotUploadedItems().length) {
      this.uploader.uploadAll();
    } 
  }
}
