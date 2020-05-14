import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Appointment } from '../appointments/appointments.component';
import { Service } from '../services/services.component';
import { Customer } from '../customers/customers.component';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-services',
  templateUrl: './add-appointment.component.html'
})
export class AddAppointmentComponent {
  public appointment: Appointment;
  public selectedCustomer: string;
  public allCustomers: Customer[];
  public selectedServices: Service[];
  public selectedService: string;
  public allServices: Service[];
  private httpClient: HttpClient;
  private baseUrl;
  private router: Router;

  constructor(router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.appointment = new Appointment();
    //this.appointment.servicesIds = new Array();
    this.httpClient = http;
    this.baseUrl = baseUrl;
    this.router = router;
    //this.selectedServices
    http.get<Service[]>(baseUrl + 'api/services').subscribe(result => {
      this.allServices = result;
    }, error => console.error(error));
    http.get<Customer[]>(baseUrl + 'api/customers').subscribe(result => {
      this.allCustomers = result;
    }, error => console.error(error));    
  }

  public addAppointment(appointment: Appointment) {
    let url = this.baseUrl + 'api/appointments';
    appointment.customerId = Number.parseInt(this.selectedCustomer);
    //appointment.servicesIds = this.selectedServices.map(s => s.id);
    appointment.servicesIds = [];
    appointment.servicesIds.push(Number.parseInt(this.selectedService));

    var isAdded = this.httpClient.post<Appointment>(url, appointment, httpOptions).subscribe(
      response => {
        this.router.navigateByUrl('/appointments');
      },
      err => console.log(err),
    );



  }
}
