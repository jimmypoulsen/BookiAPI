using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookiAPI.RESTfulService.Controllers;
using BookiAPI.RESTfulService.Models;
using BookiAPI.DataAccessLayer.Models;

namespace ViewBeveragesTests
{
    [TestClass]
    public class ViewBeveragesTest
    {
        [TestMethod]
        public void TestViewBeveragesWithGetMethod()
        {
            //Arange           
            BookiAPI.RESTfulService.Models.BeverageResponse response = new BeverageResponse();
            response.Id = 1;
            response.Name = "øl";
            response.SalesPrice = 10;
            response.Stock = 99;
            response.VenueId = 1;
            response.Description = "Smager dejlig";
            response.CostPrice = 50;

            // BookiAPI.DataAccessLayer.Models.Beverage beverage = new Beverage();
            /*beverageId = 1;
            beverage.Name = "øl";
            beverage.SalesPrice = 10;
            beverage.Stock = 99;
            beverage.VenueId = 1;
            beverage.Description = "Smager dejlig";
            beverage.CostPrice = 50;
            */

            //Act
            BookiAPI.RESTfulService.Controllers.BeveragesController




            //Assert
        }
    }
}
