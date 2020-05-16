import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-services',
  templateUrl: './customers.component.html'
})
export class CustomersComponent {
  public customers: Customer[];

  private baseUrl;
  private httpClient: HttpClient;

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = httpClient;
    this.baseUrl = baseUrl;

    httpClient.get<Customer[]>(baseUrl + 'api/customers').subscribe(result => {
      this.customers = result;
    }, error => console.error(error));
  }

  public deleteCustomer(customer: Customer) {
    let url = this.baseUrl + 'api/customers';

    this.httpClient.delete(url + "/" + customer.customerId).subscribe(
      response => {
        this.customers = this.customers.filter(c => c.customerId != customer.customerId);
      },
      err => { console.log(err) },
    );
  }
}

export class Customer {
  customerId: number;
  firstName: string;
  lastName: string;
  phone: string;
  email: string;
}
