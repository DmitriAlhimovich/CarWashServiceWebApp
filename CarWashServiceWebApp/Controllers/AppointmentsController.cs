using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CarWashServiceWebApp.Controllers;
using CarWashServiceWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarWashServiceWebApp.Models;

namespace CarWashServiceWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<ServicesController> logger;

        public AppointmentsController(ILogger<ServicesController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<AppointmentDto> Index()
        {            
            using (var context = new CarWashServiceContext())
            {
                return context.Appointments.OrderByDescending(a => a.StartTime)
                    .Select(a => new AppointmentDto
                    {
                        AppointmentId = a.AppointmentId,
                        CustomerId = a.Customer.CustomerId,
                        CustomerName = $"{a.Customer.FirstName} {a.Customer.LastName} ",
                        ServiceId = a.Service.ServiceId,
                        ServiceTitle = a.Service.Title,
                        StartTime = a.StartTime.ToShortDateString(),
                        Cost = a.Service.Price
                    }).ToList();                
            }
        }

        [HttpPost()]
        public IActionResult Add(AppointmentDto appointmentDto)
        {
            using (var context = new CarWashServiceContext())
            {
                var appointment = new Appointment
                {                    
                    Customer = context.Customers.First(c => c.CustomerId == appointmentDto.CustomerId),
                    Service = context.Services.First(c => c.ServiceId == appointmentDto.ServiceId)
                };
                if (DateTime.TryParse(appointmentDto.StartTime, out DateTime startTime))
                    appointment.StartTime = startTime;

                context.Appointments.Add(appointment);                
                context.SaveChanges();
            }

            return Ok();
        }
    }
}
