using DeveloperTest.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("Customer/Type")]
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerTypeController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        /// <summary>
        /// Get all customer types
        /// </summary>
        [HttpGet]
        public IActionResult GetCustomerTypes()
        {
            return Ok(customerService.GetCustomerTypes());
        }
    }
}