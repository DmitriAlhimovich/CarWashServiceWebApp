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
    }
}
