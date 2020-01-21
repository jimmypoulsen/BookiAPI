using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using BookiAPI.DataAccessLayer.Models;
using BookiAPI.RESTfulService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookiAPI.Seeds.Helpers;

namespace BookiAPI.Seeds
{
    class Program {
        static void Main(string[] args) {
            Database.testEnv = true;
            BeverageRepository _beverageRepository = new BeverageRepository();
            CustomerRepository _customerRepository = new CustomerRepository();
            EmployeeRepository _employeeRepository = new EmployeeRepository();
            ReservationRepository _reservationRepository = new ReservationRepository();
            TablePackageRepository _tablePackageRepository = new TablePackageRepository();
            ReservationTablePackageRepository _reservationTablePackageRepository = new ReservationTablePackageRepository();
            TableRepository _tableRepository = new TableRepository();
            VenueHourRepository _venueHourRepository = new VenueHourRepository();
            VenueEmployeeRepository _venueEmployeeRepository = new VenueEmployeeRepository();
            VenueRepository _venueRepository = new VenueRepository();

            Console.WriteLine("Seeding database ..");
            _beverageRepository.Truncate();
            _reservationTablePackageRepository.Truncate();
            _reservationRepository.Truncate();
            _tablePackageRepository.Truncate();
            _tableRepository.Truncate();
            _venueHourRepository.Truncate();
            _venueEmployeeRepository.Truncate();
            _venueRepository.Truncate();
            _customerRepository.Truncate();
            _employeeRepository.Truncate();

            Console.WriteLine("Seeding venues ..");
            Venue venue = new Venue {
                Name = "Kongebaren",
                Address = "Gammel Kongevej 1",
                Zip = 9000,
                City = "Aalborg"
            };
            venue.Id = _venueRepository.Add(venue);

            Venue venue1 = new Venue {
                Name = "Dronningenbaren",
                Address = "Gammel Kongevej 2",
                Zip = 9000,
                City = "Aalborg"
            };
            venue1.Id = _venueRepository.Add(venue1);

            Console.WriteLine("Seeding employees ..");
            Employee employee0 = new Employee
            {
                Name = "Kongen Kongessen",
                Phone = "+4598765432",
                Email = "kongen@example.com",
                Password = HashingHelper.GenerateHash("12345678"),
                EmployeeNo = 1,
                Title = "Kongen",
                Salt = HashingHelper.RandomString(20)
            };
            Employee employee1 = new Employee
            {
                Name = "Dronningen Dronningensen",
                Phone = "+4509876543",
                Email = "dronningen@example.com",
                Password = HashingHelper.GenerateHash("12345678"),
                EmployeeNo = 1,
                Title = "Dronningen",
                Salt = HashingHelper.RandomString(20),

            };

            Employee employee2 = new Employee {
                Name = "Prins Prinsen",
                Phone = "+4539847837",
                Email = "prinsen@example.com",
                Password = HashingHelper.GenerateHash("12345678"),
                EmployeeNo = 1,
                Title = "Prinsen",
                Salt = HashingHelper.RandomString(20),

            };
            employee0.Id = _employeeRepository.Add(employee0);
            employee1.Id = _employeeRepository.Add(employee1);
            employee2.Id = _employeeRepository.Add(employee2);

            Console.WriteLine("Seeding venue employees ..");
            _venueEmployeeRepository.Add(new VenueEmployee
            {
                VenueId = venue.Id,
                EmployeeId = employee0.Id,
                AccessLevel = 1
            });
            _venueEmployeeRepository.Add(new VenueEmployee
            {
                VenueId = venue1.Id,
                EmployeeId = employee1.Id,
                AccessLevel = 1
            });

            _venueEmployeeRepository.Add(new VenueEmployee {
                VenueId = venue.Id,
                EmployeeId = employee2.Id,
                AccessLevel = 1
            });

            _venueEmployeeRepository.Add(new VenueEmployee {
                VenueId = venue1.Id,
                EmployeeId = employee2.Id,
                AccessLevel = 1
            });

            Console.WriteLine("Seeding customers ..");
            string salt = HashingHelper.RandomString(20);
            Customer customer0 = new Customer {
                Name = "Kunde Kundessen",
                Phone = "+4512345678",
                Email = "kunde@example.com",
                Salt = salt,
                Password = HashingHelper.GenerateHashWithSalt("12345678", salt),
                CustomerNo = 1,
            };
            salt = HashingHelper.RandomString(20);
            Customer customer1 = new Customer
            {
                Name = "Gæst Gæstessen",
                Phone = "+4587654321",
                Email = "gaest@example.com",
                Salt = salt,
                Password = HashingHelper.GenerateHashWithSalt("87654321", salt),
                CustomerNo = 2
            };
            customer0.Id = _customerRepository.Add(customer0);
            customer1.Id = _customerRepository.Add(customer1);

            Console.WriteLine("Seeding tables ..");
            Table table0 = new Table {
                NoOfSeats = 4,
                Name = "Vinderbordet",
                VenueId = venue.Id,
            };
            Table table1 = new Table
            {
                NoOfSeats = 6,
                Name = "Taberbordet",
                VenueId = venue.Id
            };
            Table table2 = new Table {
                NoOfSeats = 7,
                Name = "Det gode bord",
                VenueId = venue1.Id,
            };
            Table table3 = new Table {
                NoOfSeats = 3,
                Name = "Det dårlige bord",
                VenueId = venue1.Id
            };
            table0.Id = _tableRepository.Add(table0);
            table1.Id = _tableRepository.Add(table1);
            table2.Id = _tableRepository.Add(table2);
            table3.Id = _tableRepository.Add(table3);

            Console.WriteLine("Seeding table packages ..");
            TablePackage tablePackage0 = new TablePackage {
                Name = "Den dyre",
                Price = 6000,
                VenueId = venue.Id,
            };
            TablePackage tablePackage1 = new TablePackage
            {
                Name = "Den billige",
                Price = 500,
                VenueId = venue.Id
            };
            TablePackage tablePackage2 = new TablePackage {
                Name = "Den Gode",
                Price = 4000,
                VenueId = venue1.Id,
            };
            TablePackage tablePackage3 = new TablePackage {
                Name = "Den Den Dårlige",
                Price = 300,
                VenueId = venue1.Id
            };
            tablePackage0.Id = _tablePackageRepository.Add(tablePackage0);
            tablePackage1.Id = _tablePackageRepository.Add(tablePackage1);
            tablePackage2.Id = _tablePackageRepository.Add(tablePackage2);
            tablePackage3.Id = _tablePackageRepository.Add(tablePackage3);

            Console.WriteLine("Seeding reservations ..");
            Reservation reservation0 = new Reservation
            {
                ReservationNo = 10001,
                DateTimeStart = Convert.ToDateTime("28/12-2019 20:00"),
                DateTimeEnd = Convert.ToDateTime("29/12-2019 05:00"),
                State = 1,
                CustomerId = customer0.Id,
                VenueId = venue.Id,
                TableId = table0.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            Reservation reservation1 = new Reservation
            {
                ReservationNo = 10002,
                DateTimeStart = Convert.ToDateTime("31/12-2019 20:00"),
                DateTimeEnd = Convert.ToDateTime("01/01-2020 05:00"),
                State = 1,
                CustomerId = customer1.Id,
                VenueId = venue1.Id,
                TableId = table1.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            reservation0.Id = _reservationRepository.Add(reservation0);
            reservation1.Id = _reservationRepository.Add(reservation1);
            _reservationTablePackageRepository.Add(new ReservationTablePackage
            {
                ReservationId = reservation0.Id,
                TablePackageId = tablePackage0.Id
            });
            _reservationTablePackageRepository.Add(new ReservationTablePackage
            {
                ReservationId = reservation1.Id,
                TablePackageId = tablePackage1.Id
            });
        }
    }
}
