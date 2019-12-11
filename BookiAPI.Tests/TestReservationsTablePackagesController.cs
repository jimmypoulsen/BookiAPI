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
    public class TestReservationsTablePackagesController
    {

        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        public void GetReservationTablePackageThatExist()
        {
            //Arange
            ReservationsTablePackagesController _reservationsTablePackagesController = new ReservationsTablePackagesController();
            int expectedReservationsTablePackagesControllerId = 1;

            //Act
            ReservationTablePackageResponse reservationTablePackageResponse = _reservationsTablePackagesController.Get(expectedReservationsTablePackagesControllerId).First();
            int actualReservationsTablePackagesControllerId = reservationTablePackageResponse.Id;

            //Assert
            Assert.AreEqual(expectedReservationsTablePackagesControllerId, actualReservationsTablePackagesControllerId);

        }


    }
}
