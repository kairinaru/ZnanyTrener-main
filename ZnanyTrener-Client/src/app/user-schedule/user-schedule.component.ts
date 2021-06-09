import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import {
  Events,
  Item,
  NgxTimeSchedulerService,
  Period,
  Section,
  Text,
} from 'ngx-time-scheduler';
import { TrainingToReturn } from '../_models/trainingToReturn';
import { UserDetail } from '../_models/userDetail';
import { AlertService } from '../_services/alert.service';
import { TrainingService } from '../_services/training.service';

@Component({
  selector: 'app-user-schedule',
  templateUrl: './user-schedule.component.html',
  styleUrls: ['./user-schedule.component.css'],
})
export class UserScheduleComponent implements OnInit {
  selectedItem: Item;
  modalRef: BsModalRef;
  events: Events = new Events();
  periods: Period[];
  sections: Section[];
  items: Item[] = [];
  text: Text;
  coach: UserDetail;
  logged: UserDetail;
  planned: TrainingToReturn[];
  @ViewChild('planned') plannedtr: TemplateRef<any>;
  stripePromise: any;
  session: any;

  constructor(
    private route: ActivatedRoute,
    private service: NgxTimeSchedulerService,
    private modalService: BsModalService,
    private trainingService: TrainingService,
    private alert: AlertService
  ) {}

  ngOnInit(): void {
    this.periods = [
      {
        name: '1 week',
        timeFrameHeaders: ['Do MMM', 'HH'],
        classes: 'period-7day',
        timeFrameOverall: 60 * 24 * 7,
        timeFramePeriod: 60 * 3,
      },
    ];

    this.sections = [
      {
        name: 'Zaplanowane treningi',
        id: 1,
      },
    ];

    this.text = {
      NextButton: 'Następny tydzień',
      PrevButton: 'Poprzedni tydzień',
      TodayButton: 'Dzisiaj',
      GotoButton: 'Przejdź do dnia',
      SectionTitle: '',
    };

    this.events.ItemClicked = (item) => {
      this.selectedItem = item;
      this.modalRef = this.modalService.show(this.plannedtr);
    };

    this.getTrainings();
  }

  getTrainings() {
    this.trainingService
      .getForUser(JSON.parse(localStorage.getItem('user')).id)
      .subscribe((data) => {
        let j = 0;
        this.planned = data;
        for (let i: number = 0; i < this.planned.length; i++) {
          this.service.itemPush({
            id: j,
            sectionID: 1,
            start: moment(this.planned[i].startDate),
            end: moment(this.planned[i].endDate),
            name: '',
            classes: '',
          });
        }
      });
  }
}
