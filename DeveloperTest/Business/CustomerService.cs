using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DeveloperTest.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public CustomerType[] GetCustomerTypes()
        {
            return context.CustomerTypes.Select(x => new CustomerType
            {
                CustomerTypeId = x.CustomerTypeId,
                Name = x.Name
            }).ToArray();
        }

        public CustomerModel[] GetCustomers()
        {
            return context.Customers
                .Include(x => x.Type)
                .Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    CustomerTypeId = x.TypeId,
                    CustomerTypeName = x.Type.Name
                }).ToArray();
        }

        public CustomerModel GetCustomer(int customerId)
        {
            return context.Customers
                .Include(x => x.Type)
                .Where(x => x.CustomerId == customerId).Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    CustomerTypeId = x.TypeId,
                    CustomerTypeName = x.Type.Name
                }).SingleOrDefault();
        }

        public CustomerModel CreateCustomer(CustomerModel model)
        {
            var customerType = context.CustomerTypes.SingleOrDefault(c => c.CustomerTypeId == model.CustomerTypeId);
            if (customerType == null)
            {
                throw new InvalidOperationException($"No customer type found with id: {model.CustomerTypeId}");
            }

            var addedCustomer = context.Customers.Add(new Customer
            {
                Name = model.Name,
                TypeId = model.CustomerTypeId
            });

            context.SaveChanges();

            return new CustomerModel
            {
                CustomerId = addedCustomer.Entity.CustomerId,
                Name = addedCustomer.Entity.Name,
                CustomerTypeId = addedCustomer.Entity.TypeId,
                CustomerTypeName = addedCustomer.Entity.Type.Name
            };
        }

        public CustomerModel UpdateCustomer(CustomerModel model)
        {
            var updatedCustomer = context.Customers.SingleOrDefault(c => c.CustomerId == model.CustomerId);
            if (updatedCustomer == null)
            {
                throw new InvalidOperationException($"No customer found with id: {model.CustomerId}");
            }

            var customerType = context.CustomerTypes.SingleOrDefault(c => c.CustomerTypeId == model.CustomerTypeId);
            if (customerType == null)
            {
                throw new InvalidOperationException($"No customer type found with id: {model.CustomerTypeId}");
            }

            updatedCustomer.Name = model.Name;
            updatedCustomer.TypeId = model.CustomerTypeId;

            context.SaveChanges();

            return new CustomerModel
            {
                CustomerId = updatedCustomer.CustomerId,
                Name = updatedCustomer.Name,
                CustomerTypeId = updatedCustomer.TypeId,
                CustomerTypeName = updatedCustomer.Type.Name
            };
        }
    }
}