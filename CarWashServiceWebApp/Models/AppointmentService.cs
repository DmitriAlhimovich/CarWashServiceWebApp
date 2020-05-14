using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWashServiceWebApp.Models
{
    [Table("AppointmentService")]
    public class AppointmentService
    {
        [Key]
        public int AppointmentServiceId { get; set; }
        public Appointment Appointment { get; set; }
        public Service Service { get; set; }
        public double Amount { get; set; }
    }
}
