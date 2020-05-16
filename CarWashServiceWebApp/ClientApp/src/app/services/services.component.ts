import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html'
})
export class ServicesComponent {
  public services: Service[];

  private baseUrl;
  private httpClient: HttpClient;

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = httpClient;
    this.baseUrl = baseUrl;

    httpClient.get<Service[]>(baseUrl + 'api/services').subscribe(result => {
      this.services = result;      
    }, error => console.error(error));
  }

  public deleteService(service: Service) {
    let url = this.baseUrl + 'api/services';

    this.httpClient.delete(url + "/" + service.serviceId).subscribe(
      response => {
        this.services = this.services.filter(s => s.serviceId != service.serviceId);
      },
      err => { console.log(err) },
    );
  }
}

export class Service {
  serviceId: number;
  title: string;
  description: string;
  price: number;
  duration: any;  
}
