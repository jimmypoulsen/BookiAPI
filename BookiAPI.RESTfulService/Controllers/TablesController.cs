using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookiAPI.RESTfulService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TablesController : ApiController
    {
        private readonly TableRepository _tableRepository;

        public TablesController()
        {
            _tableRepository = new TableRepository();
        }

        // GET /api/venue/
        public IEnumerable<TableResponse> Get()
        {
            return _tableRepository.Get().Select(table => new TableResponse
            {
                Id = table.Id,
                NoOfSeats = table.NoOfSeats,
                Name = table.Name,
                VenueId = table.VenueId
            });
        }

        // GET /api/venue/1/
        public IEnumerable<TableResponse> Get(int id)
        {
            return _tableRepository.Get(id).Select(table => new TableResponse
            {
                Id = table.Id,
                NoOfSeats = table.NoOfSeats,
                Name = table.Name,
                VenueId = table.VenueId
            });
        }

        public IEnumerable<TableResponse> GetByVenueId(int venueId)
        {
            return _tableRepository.GetByVenueId(venueId).Select(table => new TableResponse
            {
                Id = table.Id,
                NoOfSeats = table.NoOfSeats,
                Name = table.Name,
                VenueId = table.VenueId
            });
        }

        [Route("api/venues/{venueId}/available-tables")] // api/venues/1/available-tables?dateTimeStart=28-12-2019 10:00&dateTimeEnd=28-12-2019 15:00
        [HttpGet]
        public IEnumerable<TableResponse> GetAvailableTables(int venueId, string dateTimeStart, string dateTimeEnd)
        {
            return _tableRepository.GetAvailableTables(venueId, dateTimeStart, dateTimeEnd).Select(table => new TableResponse
            {
                Id = table.Id,
                NoOfSeats = table.NoOfSeats,
                Name = table.Name,
                VenueId = table.VenueId
            });
        }

        // POST /api/venue/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data)
        {
            BookiAPI.DataAccessLayer.Models.Table table = new DataAccessLayer.Models.Table
            {
                NoOfSeats = (int)data.Table.NoOfSeats.Value,
                Name = data.Table.Name.Value,
                VenueId = (int)data.Table.VenueId.Value
            };

            if (_tableRepository.Add(table) > 0)
                return Ok("Table was created");
            else
                return BadRequest("Something went wrong ..");
        }
        public IHttpActionResult Delete(int id)
        {
            if (_tableRepository.Delete(id))
                return Ok("Table was deleted");
            else
                return BadRequest("Something went wrong ..");
        }

        public bool DeleteByVenueId(int venueId)
        {
            return _tableRepository.DeleteByVenueId(venueId);
        }
    }

}
