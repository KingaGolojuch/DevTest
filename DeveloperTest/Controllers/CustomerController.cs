using DeveloperTest.Business.Interfaces;
using DeveloperTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(customerService.GetCustomers());
        }

        /// <summary>
        /// Get customer by identifier
        /// </summary>
        /// <param name="id">Customer id</param>
        [HttpGet("{id}")]
        public IActionResult Get([Required][FromRoute] int id)
        {
            var customer = customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create([Required][FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = customerService.CreateCustomer(model);

            return Created($"customer/{customer.CustomerId}", customer);
        }

        [HttpPut]
        public IActionResult Update([Required][FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = customerService.UpdateCustomer(model);

            return Ok(customer);
        }
    }
}