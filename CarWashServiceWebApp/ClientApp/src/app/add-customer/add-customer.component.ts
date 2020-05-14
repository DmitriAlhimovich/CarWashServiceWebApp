import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Customer } from '../customers/customers.component';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-services',
  templateUrl: './add-customer.component.html'
})
export class AddCustomerComponent {
  public customer: Customer;  
  private httpClient: HttpClient;
  private baseUrl;
  private router: Router;

  constructor(router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.customer = new Customer();
    this.httpClient = http;
    this.baseUrl = baseUrl;
    this.router = router;
  }

  public addCustomer(customer: Customer) {
    let url = this.baseUrl + 'api/customers';    

    this.httpClient.post<Customer>(url, customer, httpOptions).subscribe(
      response => {
        this.router.navigateByUrl('/customers');
      },
      err => { console.log(err) },
    );
  }
}
