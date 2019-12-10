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
            int expectedBeverageId = 2;

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
                BeverageResponse beverageResponse = _beveragesController.Get(2).First();
                int actualBeverageId = beverageResponse.Id;

                // assert
                Assert.AreNotEqual(notExpectedBeverageId, actualBeverageId);
            }
        }

            [TestMethod]
            public void CreateBeverageFromPostMethod()
        {
            //Arrange
            BeveragesController _beveragesController = new BeveragesController();
            _beveragesController

            //Act
            BeverageResponse beverageResponse = _beveragesController.
            //Assert

        }
            

        

        }
    }
}
