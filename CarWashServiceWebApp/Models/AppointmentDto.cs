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
        public int CustomerId { get; set; }
        public DateTime StartTime { get; set; }
        public int[] ServicesIds { get; set; }
    }
}
