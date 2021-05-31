using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business.Interfaces
{
    public interface ICustomerService
    {
        CustomerType[] GetCustomerTypes();

        CustomerModel[] GetCustomers();

        CustomerModel GetCustomer(int customerId);

        CustomerModel CreateCustomer(CustomerModel model);

        CustomerModel UpdateCustomer(CustomerModel model);
    }
}
