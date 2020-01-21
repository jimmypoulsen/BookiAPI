using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookiAPI.DataAccessLayer;
using BookiAPI.DataAccessLayer.Models;

namespace BookiAPI.Tests.Factories
{
    class CustomerFactory
    {
        public static string Name
        {
            get => "Kongen";
            set => Name = value;
        }
        public static string Phone
        {
            get => "+4500112233";
            set => Phone = value;
        }
        public static string Email
        {
            get => "kongen@example.com";
            set => Email = value;
        }
        public static string Password
        {
            get => "12345678";
            set => Password = value;
        }
        public static int CustomerNo
        {
            get => 1;
            set => CustomerNo = value;
        }
        public static string Salt
        {
            get => "109237ulaksdjf09123";
            set => Salt = value;
        }

        public static Customer Create()
        {
            CustomerRepository _customerRepository = new CustomerRepository();
            Customer customer = new Customer
            {
                Name = Name,
                Phone = Phone,
                Email = Email,
                Password = Password,
                CustomerNo = CustomerNo,
                Salt = Salt
            };
            _customerRepository.Add(customer);
            return customer;
        }
    }
}
