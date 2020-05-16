using System;
using System.Collections.Generic;
using System.Linq;
using CarWashService.Core.Data;
using CarWashService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarWashServiceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {        
        [HttpGet]
        public IEnumerable<Customer> Index()
        {
            using var context = new CarWashServiceContext();
            var list = context.Customers.ToList();

            return list;
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new CarWashServiceContext())
            {
                context.Customers.Remove(context.Customers.FirstOrDefault(c => c.CustomerId == id));
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
