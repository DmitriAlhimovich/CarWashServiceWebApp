import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html'
})
export class ServicesComponent {
  public services: Service[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Service[]>(baseUrl + 'api/services').subscribe(result => {
      this.services = result;
    }, error => console.error(error));
  }
}

export interface Service {
  serviceId: number;
  title: string;
  description: string;
  price: number;
  duration: number;
}
