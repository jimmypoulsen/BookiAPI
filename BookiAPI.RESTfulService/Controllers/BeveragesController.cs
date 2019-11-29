using BookiAPI.DataAccessLayer;
using BookiAPI.DataAccessLayer.Models;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace BookiAPI.RESTfulService.Controllers
{
    public class BeveragesController : ApiController
    {

        private readonly BeverageRepository _beverageRepository;

        public BeveragesController() {
            _beverageRepository = new BeverageRepository();
        }

        // GET /api/employees/
        public IEnumerable<BeverageResponse> Get() {
            return _beverageRepository.Get().Select(beverage => new BeverageResponse {
                Id = beverage.Id,
                Name = beverage.Name,
                Barcode = beverage.Barcode,
                Description = beverage.Description,
                CostPrice = beverage.CostPrice,
                SalesPrice = beverage.SalesPrice,
                Stock = beverage.Stock,
                VenueId = beverage.VenueId
            });
        }

        // GET /api/employees/1/
        public IEnumerable<BeverageResponse> Get(int id) {
            return _beverageRepository.Get(id).Select(beverage => new BeverageResponse {
                Id = beverage.Id,
                Name = beverage.Name,
                Barcode = beverage.Barcode,
                Description = beverage.Description,
                CostPrice = beverage.CostPrice,
                SalesPrice = beverage.SalesPrice,
                Stock = beverage.Stock,
                VenueId = beverage.VenueId
            });
        }

        public IEnumerable<BeverageResponse> GetByVenueId(int venueId)
        {
            return _beverageRepository.GetByVenueId(venueId).Select(beverage => new BeverageResponse
            {
                Id = beverage.Id,
                Name = beverage.Name,
                Barcode = beverage.Barcode,
                Description = beverage.Description,
                CostPrice = beverage.CostPrice,
                SalesPrice = beverage.SalesPrice,
                Stock = beverage.Stock,
                VenueId = beverage.VenueId
            });
        }

        // POST /api/employees/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data) {
            BookiAPI.DataAccessLayer.Models.Beverage beverage = new DataAccessLayer.Models.Beverage {
                Name = data.Beverage.Name.Value,
                Barcode = data.Beverage.Barcode.Value,
                Description = data.Beverage.Description.Value,
                CostPrice = (decimal) data.Beverage.CostPrice.Value,
                SalesPrice = (decimal) data.Beverage.SalesPrice.Value,
                Stock = (int) data.Beverage.Stock.Value,
                VenueId = (int) data.Beverage.VenueId.Value
            };

            if (_beverageRepository.Add(beverage) > 0)
                return Ok("Beverage was created");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Delete(int id) {
            if (_beverageRepository.Delete(id))
                return Ok("Beverage was deleted");
            else
                return BadRequest("Something went wrong ..");
        }
    }
}
