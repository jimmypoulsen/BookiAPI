using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookiAPI.RESTfulService.Controllers
{
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
