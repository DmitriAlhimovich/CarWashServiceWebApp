using System.Collections.Generic;
using System.Linq;
using CarWashService.Core.Data;
using CarWashService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarWashServiceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Service> Index()
        {
            using var context = new CarWashServiceContext();
            var list = context.Services.Where(s => s.IsAvailable).ToList();

            return list;
        }

        [HttpPost()]
        public IActionResult Add(Service service)
        {
            using (var context = new CarWashServiceContext())
            {
                service.IsAvailable = true;
                context.Services.Add(service);
                context.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new CarWashServiceContext())
            {
                context.Services.Remove(context.Services.FirstOrDefault(s => s.ServiceId == id));
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
