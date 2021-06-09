import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import {
  Item,
  Period,
  Section,
  Events,
  NgxTimeSchedulerService,
  Text,
} from 'ngx-time-scheduler';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { CertificateService } from '../_services/certificate.service';
import { TrainingService } from '../_services/training.service';
import { TrainingToReturn } from '../_models/trainingToReturn';
import { UserDetail } from '../_models/userDetail';
import { ActivatedRoute } from '@angular/router';
import { TrainingToAdd } from '../_models/trainingToAdd';
import { de } from 'date-fns/locale';
import { AlertService } from '../_services/alert.service';
import { PaymentService } from '../_services/payment.service';
import { loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-add-visit',
  templateUrl: './add-visit.component.html',
  styleUrls: ['./add-visit.component.css'],
})
export class AddVisitComponent implements OnInit {
  @ViewChild('preview') templ: TemplateRef<any>;
  @ViewChild('planned') plannedtr: TemplateRef<any>;
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
  stripePromise: any;
  session: any;

  constructor(
    private route: ActivatedRoute,
    private service: NgxTimeSchedulerService,
    private modalService: BsModalService,
    private trainingService: TrainingService,
    private alert: AlertService,
    private payment: PaymentService
  ) {}

  ngOnInit(): void {
    localStorage.removeItem('training_to_add');
    this.stripePromise = loadStripe(
      'pk_test_51I5YvcJvKXNzTQcriYlZbz2AY6gfE97xSpJ7mUaL2UWOAABOjPBN9mlS3421iRf2nL25anPw6bgSSP5mxg7yI1Z600SJy8Gsm7'
    );
    this.route.data.subscribe((data) => {
      this.coach = data['user'];
      this.logged = JSON.parse(localStorage.getItem('user'));
      this.deletePlanned();
    });

    this.events.SectionClickEvent = (section) => {
      console.log(section);
    };
    this.events.ItemClicked = (item) => {
      if (item.sectionID === 1 && this.logged.id !== this.coach.id) {
        this.selectedItem = item;
        this.modalRef = this.modalService.show(this.templ);
      }
      else if (item.sectionID === 2 && this.logged.id === this.coach.id) {
        this.selectedItem = item;
        this.modalRef = this.modalService.show(this.plannedtr);
      }
      else if (item.sectionID === 1 && this.logged.id === this.coach.id) {
      }
      else {
        this.alert.message('Termin jest już zajęty');
      }
    };
    this.events.ItemDropped = (item) => {
      console.log(item);
    };

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
        name: 'Wolne terminy',
        id: 1,
      },
      {
        name: 'Zajęte terminy',
        id: 2,
      },
    ];

    this.text = {
      NextButton: 'Następny tydzień',
      PrevButton: 'Poprzedni tydzień',
      TodayButton: 'Dzisiaj',
      GotoButton: 'Przejdź do dnia',
      SectionTitle: 'Wolne terminy',
    };
    this.fillItems();
  }

  addItem() {
    this.service.itemPush({
      id: 4,
      sectionID: 1,
      name: 'Item 4',
      start: moment('2021-01-11 09:00'),
      end: moment('2021-01-11 10:20').add(5, 'hours'),
      classes: '',
    });
  }

  popItem() {
    this.service.itemPop();
  }

  removeItem() {
    this.service.itemRemove(4);
  }

  fillItems() {
    let year: number = 2021;
    let month: number = 1;
    let day: number = 1;

    let itemMoment = moment('2021-' + month + day);
    let countID = 0;
    for (let i = 1; i <= 12; i++) {
      if (
        i === 1 ||
        i === 3 ||
        i === 5 ||
        i === 7 ||
        i === 8 ||
        i === 10 ||
        i === 12
      ) {
        for (let j = 1; j <= 31; j++) {
          for (let k = 8; k <= 16; k = k + 2) {
            itemMoment = moment(
              '2021-' + i + '-' + j + ' ' + k + ':00' + ':00'
            );
            countID = countID + 1;
            this.items.push({
              id: countID,
              sectionID: 1,
              name: 'Item' + countID,
              start: itemMoment.utc(),
              end: moment(
                '2021-' + i + '-' + j + ' ' + (k + 1) + ':45' + ':00'
              ).utc(),
              classes: '',
            });
          }
        }
      } else if (i === 2) {
        for (let j = 1; j <= 28; j++) {
          for (let k = 8; k <= 16; k = k + 2) {
            itemMoment = moment(
              '2021-' + i + '-' + j + ' ' + k + ':00' + ':00'
            );
            countID = countID + 1;
            this.items.push({
              id: countID,
              sectionID: 1,
              name: 'Item' + countID,
              start: itemMoment.utc(),
              end: moment(
                '2021-' + i + '-' + j + ' ' + (k + 1) + ':45' + ':00'
              ).utc(),
              classes: '',
            });
          }
        }
      } else {
        for (let j = 1; j <= 30; j++) {
          for (let k = 8; k <= 16; k = k + 2) {
            itemMoment = moment(
              '2021-' + i + '-' + j + ' ' + k + ':00' + ':00'
            );
            countID = countID + 1;
            this.items.push({
              id: countID,
              sectionID: 1,
              name: 'Item' + countID,
              start: itemMoment.utc(),
              end: moment(
                '2021-' + i + '-' + j + ' ' + (k + 1) + ':45' + ':00'
              ).utc(),
              classes: '',
            });
          }
        }
      }
    }
  }

  getElementByStartAndEndDate() {
    let result: number = this.findItemIndex(
      new Date('2021-1-12 08:00'),
      new Date('2021-1-12 09:45')
    );
    let result2 = this.findItemIndex(
      new Date('2021-1-13 08:00'),
      new Date('2021-1-13 09:45')
    );
    let result3 = this.findItemIndex(
      new Date('2021-1-14 08:00'),
      new Date('2021-1-14 09:45')
    );
    this.remove(result);
    this.remove(result2);
    this.remove(result3);
    debugger;
  }

  findItemIndex(start: Date, end: Date): number {
    return this.items.find(
      (x) =>
        x.start.toDate().toUTCString() ===
          moment(start).toDate().toUTCString() &&
        x.end.toDate().toUTCString() === moment(end).toDate().toUTCString()
    ).id;
  }

  remove(index: number) {
    this.service.itemRemove(index);
  }

  deletePlanned() {
    this.trainingService.getForCoach(this.coach.id).subscribe(
      (data) => {
        this.planned = data;
        let index: number = 0;
        debugger;
        for (let i = 0; i < this.planned.length; i++) {
          let x = this.findItemIndex(
            this.planned[i].startDate,
            this.planned[i].endDate
          );

          let y = this.planned[i];
          this.remove(x);

          this.service.itemPush({
            id: index,
            sectionID: 2,
            name: '',
            start: moment(y.startDate),
            end: moment(y.endDate),
            classes: '',
          });
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  goToCheckout() {
    const x: any = { amount: 1000 };
    this.payment.charge().subscribe(
      async (data) => {
        this.session = data;
        const stripe = await this.stripePromise;

        //Dodawanie treningu do localStorage
        const userId = JSON.parse(localStorage.getItem('user')).id;
        const startDate = this.selectedItem.start.utc().toDate();
        const endDate = this.selectedItem.end.utc().toDate();
        const trainingToAdd: TrainingToAdd = {
          userId: userId,
          coachId: this.coach.id,
          startDate: startDate,
          endDate: endDate,
        };

        localStorage.setItem('training_to_add', JSON.stringify(trainingToAdd));

        //Przekierowanie do płatności
        stripe
          .redirectToCheckout({
            sessionId: this.session.id,
          })
          .subsribe((data) => {});

        /*if(result.error) {
          this.alert.error(result.error.message);
        } else {
          this.addTraining();
        }*/
      },
      (error) => this.alert.error(error)
    );
  }
}
