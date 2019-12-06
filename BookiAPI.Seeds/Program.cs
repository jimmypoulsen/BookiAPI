using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using BookiAPI.DataAccessLayer.Models;
using BookiAPI.RESTfulService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.Seeds
{
    class Program {
        static void Main(string[] args) {
            CustomersController customersController = new CustomersController();
            BeverageRepository _beverageRepository = new BeverageRepository();
            CustomerRepository _customerRepository = new CustomerRepository();
            EmployeeRepository _employeeRepository = new EmployeeRepository();
            ReservationRepository _reservationRepository = new ReservationRepository();
            TablePackageRepository _tablePackageRepository = new TablePackageRepository();
            TableRepository _tableRepository = new TableRepository();
            VenueHourRepository _venueHourRepository = new VenueHourRepository();
            VenueRepository _venueRepository = new VenueRepository();

            Console.WriteLine("Seeding database ..");
            _beverageRepository.Truncate();
            _tableRepository.Truncate();
            _venueRepository.Truncate();
            _employeeRepository.Truncate();

            Console.WriteLine("Seeding venues ..");
            Venue venue = new Venue {
                Name = "Kongebaren",
                Address = "Gammel Kongevej 1",
                Zip = 9000,
                City = "Aalborg"
            };
            venue.Id = _venueRepository.Add(venue);

            Console.WriteLine("Seeding employees ..");
            Employee employee = new Employee {
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
            Beverage beverage = new Beverage {
                Name = "Kongedrink",
                Barcode = "1234567890987654321",
                Description = "En konge værdig",
                CostPrice = 20,
                SalesPrice = 45,
                Stock = 100,
                VenueId = venue.Id
            };
            _beverageRepository.Add(beverage);

            Console.WriteLine("Seeding customers ..");
            Customer customer = new Customer {
                Name = "Kunde Kundessen",
                Phone = "+4512345678",
                Email = "kunde@example.com",
                Password = "12345678",
                CustomerNo = 1,
            };
            _customerRepository.Add(customer);

            CustomerResponse cr = customersController.GetByEmail("kunde@example.com").First();
            Console.WriteLine(cr.Email);

            Console.WriteLine("Seeding tables ..");
            Table table = new Table {
                NoOfSeats = 4,
                Name = "Vinderbordet",
                VenueId = venue.Id,
            };
            _tableRepository.Add(table);

            Console.WriteLine("Seeding table packages ..");
            TablePackage tablePackage = new TablePackage {
                Name = "Den dyre",
                Price = 6000,
                VenueId = venue.Id,
            };
            _tablePackageRepository.Add(tablePackage);
         }
    }
}
