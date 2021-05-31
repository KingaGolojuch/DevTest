import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CustomerModel } from '../models/customer.model';
import { Observable } from 'rxjs';
import { CustomerTypeModel } from '../models/customer-type.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private customerBaseUrl = 'http://localhost:63235/customer';

  public constructor(private httpClient: HttpClient) { }

  public GetCustomerTypes(): Observable<CustomerTypeModel[]> {
    return this.httpClient.get<CustomerTypeModel[]>(`${this.customerBaseUrl}/type`);
  }

  public GetCustomers(): Observable<CustomerModel[]> {
    return this.httpClient.get<CustomerModel[]>(this.customerBaseUrl);
  }

  public GetCustomer(customerId: number): Observable<CustomerModel> {
    return this.httpClient.get<CustomerModel>(`${this.customerBaseUrl}/${customerId}`);
  }

  public CreateCustomer(customer: CustomerModel): Observable<CustomerModel> {
    return this.httpClient.post<CustomerModel>(this.customerBaseUrl, customer);
  }

  public UpdateCustomer(customer: CustomerModel): Observable<CustomerModel> {
    return this.httpClient.put<CustomerModel>(this.customerBaseUrl, customer);
  }
}
