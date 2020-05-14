import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-services',
  templateUrl: './appointments.component.html'
})
export class AppointmentsComponent {
  public appointments: Appointment[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Appointment[]>(baseUrl + 'api/appointments').subscribe(result => {
      this.appointments = result;
    }, error => console.error(error));
  }
}

export class Appointment {
  customerId: number;
  startTime: Date;
  servicesIds: number[];     
}
