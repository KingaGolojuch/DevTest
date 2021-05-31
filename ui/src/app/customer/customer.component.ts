import { Component, OnInit } from '@angular/core';
import { CustomerModel } from '../models/customer.model';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  public customers: CustomerModel[] = [];

  public constructor(
    private customerService: CustomerService) { }

  public ngOnInit(): void {
    this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
  }
}
