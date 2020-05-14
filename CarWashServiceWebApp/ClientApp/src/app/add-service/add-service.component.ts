import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Service } from '../services/services.component';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-services',
  templateUrl: './add-service.component.html'
})
export class AddServiceComponent {
  public service: Service;  
  private httpClient: HttpClient;
  private baseUrl;
  private router: Router;

  constructor(router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.service = new Service();    
    this.httpClient = http;
    this.baseUrl = baseUrl;
    this.router = router;    
  }

  public addService(service: Service) {
    let url = this.baseUrl + 'api/services';    

    var isAdded = this.httpClient.post<Service>(url, service, httpOptions).subscribe(
      response => {
        this.router.navigateByUrl('/services');
      },
      err => { console.log(err) },
    );



  }
}
