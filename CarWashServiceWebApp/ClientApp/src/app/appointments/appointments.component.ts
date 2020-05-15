import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { MatTabsModule, MatTabGroup } from '@angular/material/tabs';
import { groupBy } from 'rxjs/internal/operators/groupBy';

@Component({
  selector: 'app-services',
  templateUrl: './appointments.component.html'
})
export class AppointmentsComponent {
  public appointments: Appointment[];
  public appointmentsTreeByCustomers: AppointmentNode[];
  public appointmentsTreeByServices: AppointmentNode[];
  private _transformer = (node: AppointmentNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.title,
      level: level,
    };
  }

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.appointmentsTreeByServices = [];
    http.get<Appointment[]>(baseUrl + 'api/appointments').subscribe(result => {
      this.appointments = result;
      let groupByRes = this.appointments.reduce(function (h, obj) {
        h[obj.serviceTitle] = (h[obj.serviceTitle] || []).concat(obj);
        return h;
      }, {});
      
      Object.keys(groupByRes).forEach((key) => {
        let appointmentNode = new AppointmentNode(" ".concat(key));        
        appointmentNode.children = groupByRes[key].map(a => new AppointmentNode(" " + a.startTime + " " + a.customerName));
        this.appointmentsTreeByServices.push(appointmentNode);
      }
        
      );
      this.dataSource.data = this.appointmentsTreeByServices;
    }, error => console.error(error));
  };
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;


}

export class Appointment {
  customerId: number;
  startTime: string;
  customerName: string;
  serviceId: number;
  serviceTitle: string;
  cost: number;
}

class AppointmentNode {
  title: string;
  children?: AppointmentNode[];

  constructor(title: string) {
    this.title = title;
  }  
}

interface FoodNode {
  name: string;
  children?: FoodNode[];
}

const TREE_DATA: FoodNode[] = [
  {
    name: 'Fruit',
    children: [
      { name: 'Apple' },
      { name: 'Banana' },
      { name: 'Fruit loops' },
    ]
  }, {
    name: 'Vegetables',
    children: [
      {
        name: 'Green',
        children: [
          { name: 'Broccoli' },
          { name: 'Brussels sprouts' },
        ]
      }, {
        name: 'Orange',
        children: [
          { name: 'Pumpkins' },
          { name: 'Carrots' },
        ]
      },
    ]
  },
];

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}
