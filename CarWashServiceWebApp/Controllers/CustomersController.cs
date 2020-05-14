using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashServiceWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarWashServiceWebApp.Models;

namespace CarWashServiceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Customer> Index()
        {
            using (var context = new CarWashServiceContext())
            {
                var list = context.Customers.ToList();

                return list;
            }
        }

        [HttpPost()]
        public IActionResult Add(Customer customer)
        {
            using (var context = new CarWashServiceContext())
            {
                customer.RegistrationDate = DateTime.Now;
                context.Customers.Add(customer);
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
