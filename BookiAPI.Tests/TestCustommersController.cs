using BookiAPI.RESTfulService.Controllers;
using BookiAPI.RESTfulService.Models;
using BookiAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookiAPI.Tests
{
    [TestClass]
    public class TestCustommersController
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void GetCustommerThatExists()
        {
            //Arrange
            CustomersController _customersController = new CustomersController();
            int expectedCustomerId = 1;

            //Act
            CustomerResponse customereResponse = _customersController.Get(expectedCustomerId).First();
            int actualCustomerId = customereResponse.Id;
            //Assert
            Assert.AreEqual(expectedCustomerId, actualCustomerId);
        }

        [TestMethod]
        public void GetCustomerThatDosentExist()
        {
            //Arange
            CustomersController _customersController = new CustomersController();
            int notExpectedCustommerId = 99;

            //Act
            CustomerResponse customerResponse = _customersController.Get(1).First();
            int acutalCustomerId = customerResponse.Id;

            //Assert
            Assert.AreNotEqual(notExpectedCustommerId, acutalCustomerId);
        }


    }
}
