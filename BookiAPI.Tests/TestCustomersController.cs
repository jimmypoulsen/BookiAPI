using BookiAPI.RESTfulService.Controllers;
using BookiAPI.RESTfulService.Models;
using BookiAPI.DataAccessLayer.Models;
using BookiAPI.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BookiAPI.Tests
{
    [TestClass]
    public class TestCustomersController
    {
        [TestInitialize]
        public void Setup()
        {
            Database.testEnv = true;
            CustomerRepository _customerRepository = new CustomerRepository();
            _customerRepository.Truncate();
        }

        [TestMethod]
        public void GetCustomerThatExists()
        {
            // Arrange
            CustomersController _customersController = new CustomersController();
            Customer customer = Factories.CustomerFactory.Create();
            string expectedName = customer.Name;

            //Act
            CustomerResponse customerResponse = _customersController.Get().First();
            //Assert
            Assert.AreEqual(expectedName, customerResponse.Name);
        }

        [TestMethod]
        public void GetCustomerThatDoesntExists()
        {
            CustomersController _customersController = new CustomersController();
            int expectedCustomers = 0;

            IEnumerable<CustomerResponse> customers = _customersController.Get(1);

            Assert.AreEqual(expectedCustomers, customers.Count());
        }
    }
}
