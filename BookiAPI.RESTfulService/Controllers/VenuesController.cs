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
    public class VenuesController : ApiController
    {
        private readonly VenueRepository _venueRepository;
        private readonly BeveragesController _beveragesController;
        private readonly VenueHoursController _venueHoursController;

        public VenuesController()
        {
            _venueRepository = new VenueRepository();
            _beveragesController = new BeveragesController();
            _venueHoursController = new VenueHoursController();
        }

        // GET /api/venue/
        public IEnumerable<VenueResponse> Get()
        {
            return _venueRepository.Get().Select(venue => new VenueResponse
            {
                Id = venue.Id,
                Address = venue.Address,
                City = venue.City,
                Zip = venue.Zip,
                Beverages = _beveragesController.GetByVenueId(venue.Id),
                VenueHours = _venueHoursController.GetByVenueId(venue.Id)
            });
        }

        // GET /api/venue/1/
        public IEnumerable<VenueResponse> Get(int id)
        {
            return _venueRepository.Get(id).Select(venue => new VenueResponse
            {
                Id = venue.Id,
                Address = venue.Address,
                City = venue.City,
                Zip = venue.Zip,
                Beverages = _beveragesController.GetByVenueId(venue.Id),
                VenueHours = _venueHoursController.GetByVenueId(venue.Id)
            });
        }

        // POST /api/venue/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data)
        {
            BookiAPI.DataAccessLayer.Models.Venue venue = new DataAccessLayer.Models.Venue
            {
                Address = data.Venue.Address.Value,
                City = data.Venue.City.Value,
                Zip = (int)data.Venue.Zip.Value,
                VenueId = (int)data.VenueId.Value
            };

            if (_venueRepository.Add(venue))
                return Ok("Venue was created");
            else
                return BadRequest("Something went wrong ..");
        }
    }
}