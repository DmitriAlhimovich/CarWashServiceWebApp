using CarWashService.Core.Data;
using CarWashService.Core.Models;
using System;
using System.Linq;

namespace CarWashServiceWebApp
{
    public class TestDataGenerator
    {
        public void GenerateTestData()
        {
            GenerateCustomers();
            GenerateServices();
            GenerateAppointments();
        }

        private void GenerateCustomers()
        {
            string[] firstNames = new[] { "John", "James", "Richard", "George", "Donald" };
            string[] lastNames = new[] { "Black", "Green", "Bing", "Paul", "Brams" };

            Random random1 = new Random(DateTime.Now.Second);
            Random random2 = new Random(DateTime.Now.Minute);

            using var context = new CarWashServiceContext();
            if (context.Customers.Any()) return;

            for (int i = 0; i < 20; i++)
                context.Customers.Add(new Customer
                {
                    FirstName = firstNames[random1.Next(firstNames.Length)],
                    LastName = lastNames[random2.Next(lastNames.Length)]
                });

            context.SaveChanges();
        }

        private void GenerateServices()
        {
            using var context = new CarWashServiceContext();
            if (context.Services.Any())
                return;

            context.Services.Add(new Service { Title = "Hand wash", IsAvailable = true, Duration = new TimeSpan(0, 40, 0) });
            context.Services.Add(new Service { Title = "Wax polish", IsAvailable = true, Duration = new TimeSpan(0, 20, 0) });
            context.Services.Add(new Service { Title = "Wash and Vac", IsAvailable = true, Duration = new TimeSpan(0, 30, 0) });
            context.Services.Add(new Service { Title = "Interior cleaning", IsAvailable = true, Duration = new TimeSpan(0, 35, 0) });
            context.Services.Add(new Service { Title = "Interior and exterior", IsAvailable = true, Duration = new TimeSpan(0, 60, 0) });

            context.SaveChanges();
        }

        private void GenerateAppointments()
        {
            Random random1 = new Random(42);
            Random random2 = new Random(55);
            Random random3 = new Random(67);

            using var context = new CarWashServiceContext();
            if (context.Appointments.Any())
                return;            

            for (int i = 0; i < 10; i++)
            {
                var randomDaysAgo = -random1.Next(1, 5);
                var randomServiceId = random2.Next(1, 4);
                var randomCustomerId = random3.Next(1, 10);
                
                context.Appointments.Add(new Appointment
                {
                    StartTime = DateTime.Now.AddDays(randomDaysAgo),
                    Service = context.Services.First(s => s.ServiceId == randomServiceId),
                    Customer = context.Customers.First(c => c.CustomerId == randomCustomerId),
                    Amount = 1
                });
            }

            context.SaveChanges();
        }
    }
}
