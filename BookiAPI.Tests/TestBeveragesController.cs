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
    public class TestBeveragesControllers
    {
        [TestInitialize]
        public void Setup()
        {
            
        }

        [TestMethod]
        public void GetBeverageThatExists()
        {
            // arrange
            BeveragesController _beveragesController = new BeveragesController();
            int expectedBeverageId = 1;

            // act
            BeverageResponse beverageResponse = _beveragesController.Get(expectedBeverageId).First();
            int actualBeverageId = beverageResponse.Id;

            // assert
            Assert.AreEqual(expectedBeverageId, actualBeverageId);
           
        }

        [TestMethod]
        public void GetBeverageThatDoesNotExist()
        {
            {
                // arrange
                BeveragesController _beveragesController = new BeveragesController();
                int notExpectedBeverageId = 99;

                // act
                BeverageResponse beverageResponse = _beveragesController.Get(1).First();
                int actualBeverageId = beverageResponse.Id;

                // assert
                Assert.AreNotEqual(notExpectedBeverageId, actualBeverageId);
            }

        }
    }
}
