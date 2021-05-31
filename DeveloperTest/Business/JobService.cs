using System;
using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs
                .Include(x => x.Customer)
                .ThenInclude(x => x.Type)
                .Select(x => new JobModel
                {
                    JobId = x.JobId,
                    Engineer = x.Engineer,
                    When = x.When,
                    Customer = x.Customer == null ? null : new CustomerModel
                    {
                        CustomerId = x.Customer.CustomerId,
                        Name = x.Customer.Name,
                        CustomerTypeId = x.Customer.TypeId,
                        CustomerTypeName = x.Customer.Type.Name
                    }
                }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs
                .Include(x => x.Customer)
                .ThenInclude(x => x.Type)
                .Where(x => x.JobId == jobId).Select(x => new JobModel
                {
                    JobId = x.JobId,
                    Engineer = x.Engineer,
                    When = x.When,
                    Customer = x.Customer == null ? null : new CustomerModel
                    {
                        CustomerId = x.Customer.CustomerId,
                        Name = x.Customer.Name,
                        CustomerTypeId = x.Customer.TypeId,
                        CustomerTypeName = x.Customer.Type.Name
                    }
                }).SingleOrDefault();
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var customer = context.Customers.SingleOrDefault(c => c.CustomerId == model.CustomerId);
            if (customer == null)
            {
                throw new InvalidOperationException($"No customer found with id: {model.CustomerId}");
            }

            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                When = model.When,
                CustomerId = model.CustomerId
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When,
                Customer = addedJob.Entity.Customer == null ? null : new CustomerModel
                {
                    CustomerId = addedJob.Entity.Customer.CustomerId,
                    Name = addedJob.Entity.Customer.Name,
                    CustomerTypeId = addedJob.Entity.Customer.TypeId
                }
            };
        }
    }
}
