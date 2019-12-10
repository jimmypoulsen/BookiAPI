using BookiAPI.RESTfulService.Controllers;
using BookiAPI.RESTfulService.Models;
using BookiAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BookiAPI.Tests
{
    [TestClass]
    public class TestEmployeesController
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]

        public void GetEmployeeThatExist()
        {
            //arrange
            EmployeesController _employeesController = new EmployeesController();
            int expectedEmployeeId = 1;

            //act
            EmployeeResponse employeeResponse = _employeesController.Get(expectedEmployeeId).First();
            int acutalEmployeeId = employeeResponse.Id;

            //assert
            Assert.AreEqual(expectedEmployeeId, acutalEmployeeId);

        }

        [TestMethod]
        public void GetEmployeeThatDoesNotExcist()
        {
            //arrange
            EmployeesController _employeesController = new EmployeesController();
            int expectedEmployeeId = 99;

            //act
            EmployeeResponse employeeResponse = _employeesController.Get(1).First();
            int acutalEmployeeId = employeeResponse.Id;

            //assert
            Assert.AreNotEqual(expectedEmployeeId, acutalEmployeeId);
        }


    }

}
