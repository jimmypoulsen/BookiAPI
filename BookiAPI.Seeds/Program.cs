using BookiAPI.DataAccessLayer;
using BookiAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.Seeds
{
    class Program
    {
        static void Main(string[] args)
        {
            BeverageRepository _beverageRepository = new BeverageRepository();
            CustomerRepository _customerRepository = new CustomerRepository();
            EmployeeRepository _employeeRepository = new EmployeeRepository();
            ReservationRepository _reservationRepository = new ReservationRepository();
            TablePackageRepository _tablePackagerepository = new TablePackageRepository();
            VenueHourRepository _venueHourRepository = new VenueHourRepository();
            VenueRepository _venueRepository = new VenueRepository();

            Console.WriteLine("Seeding database ..");
            _beverageRepository.Truncate();
            _venueRepository.Truncate();
            _employeeRepository.Truncate();

            Console.WriteLine("Seeding venues ..");
            Venue venue = new Venue
            {
                Name = "Kongebaren",
                Address = "Gammel Kongevej 1",
                Zip = 9000,
                City = "Aalborg"
            };
            venue.Id = _venueRepository.Add(venue);

            Console.WriteLine("Seeding employees ..");
            Employee employee = new Employee
            {
                Name = "Kongen Kongessen",
                Phone = "+4512345678",
                Email = "kongen@kongehuset.dk",
                Password = "12345678",
                EmployeeNo = 1,
                Title = "Bestyrer",
                AccessLevel = 1
            };
            _employeeRepository.Add(employee);

            Console.WriteLine("Seeding beverages ..");
            Beverage beverage = new Beverage
            {
                Name = "Kongedrink",
                Barcode = "1234567890987654321",
                Description = "En konge værdig",
                CostPrice = 20,
                SalesPrice = 45,
                Stock = 100,
                VenueId = venue.Id
            };
            _beverageRepository.Add(beverage);
        }
    }
}
