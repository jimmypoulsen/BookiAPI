using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookiAPI.RESTfulService.Controllers {
    public class VenueHoursController : ApiController {
        private readonly VenueHourRepository _venueHourRepository;

        public VenueHoursController() {
            _venueHourRepository = new VenueHourRepository();
        }

        // GET /api/venue/
        public IEnumerable<VenueHourResponse> Get() {
            return _venueHourRepository.Get().Select(venueHour => new VenueHourResponse {
                Id = venueHour.Id,
                WeekDay = venueHour.WeekDay,
                OpenTime = venueHour.OpenTime,
                CloseTime = venueHour.CloseTime,
                VenueId = venueHour.VenueId
            });
        }

        // GET /api/venue/1/
        public IEnumerable<VenueHourResponse> Get(int id) {
            return _venueHourRepository.Get().Select(venueHour => new VenueHourResponse {
                Id = venueHour.Id,
                WeekDay = venueHour.WeekDay,
                OpenTime = venueHour.OpenTime,
                CloseTime = venueHour.CloseTime,
                VenueId = venueHour.VenueId
            });
        }

        public IEnumerable<VenueHourResponse> GetByVenueId(int venueId) {
            return _venueHourRepository.GetByVenueId(venueId).Select(venueHour => new VenueHourResponse {
                Id = venueHour.Id,
                WeekDay = venueHour.WeekDay,
                OpenTime = venueHour.OpenTime,
                CloseTime = venueHour.CloseTime,
                VenueId = venueHour.VenueId
            });
        }

        // POST /api/venue/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data) {
            BookiAPI.DataAccessLayer.Models.VenueHour venueHour = new DataAccessLayer.Models.VenueHour {
                WeekDay = data.VenueHour.WeekDay.Value,
                OpenTime = data.VenueHour.OpenTime.Value,
                CloseTime = data.VenueHour.CloseTime.Value,
                VenueId = (int)data.VenueHour.VenueId.Value
            };

            if (_venueHourRepository.Add(venueHour) > 0)
                return Ok("VenueHour was created");
            else
                return BadRequest("Something went wrong ..");
        }
        public IHttpActionResult Delete(int id) {
            if (_venueHourRepository.Delete(id))
                return Ok("Venuehour was deleted");
            else
                return BadRequest("Something went wrong ..");
        }

        public bool DeleteByVenueId(int venueId)
        {
            return _venueHourRepository.DeleteByVenueId(venueId);
        }
    }

}
