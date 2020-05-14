using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashServiceWebApp.Models
{
    public class AppointmentDto
    {
        
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comments { get; set; }
        public int ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public double Amount { get; set; }
        public decimal Cost { get; internal set; }
    }
}
