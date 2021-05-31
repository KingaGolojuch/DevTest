import { Component, OnInit } from '@angular/core';
import { CustomerModel } from '../models/customer.model';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../services/customer.service';
import { CustomerTypeModel } from '../models/customer-type.model';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit {

  public customer: CustomerModel = {} as CustomerModel;
  public customerTypes: CustomerTypeModel[];
  public customerFormGroup = new FormGroup({
    customerName: new FormControl('', [Validators.required, Validators.minLength(5)]),
    customerType: new FormControl('', [Validators.required]),
  });

  public constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.customerService.GetCustomerTypes().subscribe(types => this.customerTypes = types);
    this.route.params.subscribe(params => {
      const customerId = Number(params.id);

      if (customerId && !isNaN(customerId)) {
        this.customerService.GetCustomer(customerId).subscribe(customer => {
          this.customer = customer;

          this.customerFormGroup.setValue({
            customerName: customer.name,
            customerType: customer.customerTypeId
          });
        });
      }
    });
  }

  public get formControls() {
    return this.customerFormGroup.controls;
  }

  public Save(): void {
    if (this.customerFormGroup.invalid) {
      return;
    }

    const customerModel: CustomerModel = {
      customerId : this.customer.customerId,
      name: this.customerFormGroup.get('customerName').value,
      customerTypeId: Number(this.customerFormGroup.get('customerType').value),
      customerTypeName: null
    };

    if (this.customer.customerId) {
      this.customerService.UpdateCustomer(customerModel).subscribe(customer => {
        this.router.navigate(['/customers']);
      });
    } else {
      this.customerService.CreateCustomer(customerModel).subscribe(customer => {
        this.router.navigate(['/customers']);
      });
    }
  }
}
