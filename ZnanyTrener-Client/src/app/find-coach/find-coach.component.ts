import { Component, OnInit } from '@angular/core';
import { UserDetail } from '../_models/userDetail';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-find-coach',
  templateUrl: './find-coach.component.html',
  styleUrls: ['./find-coach.component.css']
})
export class FindCoachComponent implements OnInit {

  keyWord: string = '';
  coaches: UserDetail[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
  }

  filter() {
    if (this.keyWord.length >= 2) {
      this.userService.getCoaches(this.keyWord).subscribe((data) => {
        this.coaches = data;
      },
      error => {
        console.log(error);
      });
    }
    else this.coaches = [];
  }

}
