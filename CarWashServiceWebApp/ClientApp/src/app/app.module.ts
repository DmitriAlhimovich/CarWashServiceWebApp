import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ServicesComponent } from './services/services.component';
import { CustomersComponent } from './customers/customers.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { AddAppointmentComponent } from './add-appoinment/add-appointment.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { AddServiceComponent } from './add-service/add-service.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatSliderModule } from '@angular/material/slider';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ServicesComponent,
    CustomersComponent,
    AppointmentsComponent,
    AddAppointmentComponent,
    AddCustomerComponent,
    AddServiceComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'services', component: ServicesComponent },
      { path: 'customers', component: CustomersComponent },
      { path: 'appointments', component: AppointmentsComponent },
      { path: 'add-appointment', component: AddAppointmentComponent },
      { path: 'add-customer', component: AddCustomerComponent },
      { path: 'add-service', component: AddServiceComponent },
    ]),
    BrowserAnimationsModule,
    MatSliderModule,
    MatTreeModule,
    MatIconModule,
    MatTabsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
