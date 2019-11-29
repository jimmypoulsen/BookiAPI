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
    public class ReservationsController : ApiController
    {

        private readonly ReservationRepository _reservationRepository;

        public ReservationsController() {
            _reservationRepository = new ReservationRepository();
        }

        // GET /api/employees/
        public IEnumerable<ReservationResponse> Get() {
            return _reservationRepository.Get().Select(reservation => new ReservationResponse {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                DateTimeStart = reservation.DateTimeStart,
                DateTimeEnd = reservation.DateTimeEnd,
                State = reservation.State,
                CustomerId = reservation.CustomerId,
                VenueId = reservation.VenueId,
                CreatedAt = reservation.CreatedAt,
                UpdatedAt = reservation.UpdatedAt
            });
        }

        // GET /api/employees/1/
        public IEnumerable<ReservationResponse> Get(int id) {
            return _reservationRepository.Get(id).Select(reservation => new ReservationResponse {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                DateTimeStart = reservation.DateTimeStart,
                DateTimeEnd = reservation.DateTimeEnd,
                State = reservation.State,
                CustomerId = reservation.CustomerId,
                VenueId = reservation.VenueId,
                CreatedAt = reservation.CreatedAt,
                UpdatedAt = reservation.UpdatedAt
            });
        }

        // POST /api/employees/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data) {
            BookiAPI.DataAccessLayer.Models.Reservation reservation = new DataAccessLayer.Models.Reservation {
                ReservationNo = (int) data.Reservation.ReservationNo.Value,
                DateTimeStart = data.Reservation.DateTimeStart.Value,
                DateTimeEnd = data.Reservation.DateTimeEnd.Value,
                State = (int) data.Reservation.State.Value,
                CustomerId = (int) data.Reservation.CustomerId.Value,
                VenueId = (int) data.Reservation.VenueId.Value,
                CreatedAt = data.Reservation.CreatedAt.Value,
                UpdatedAt = data.Reservation.UpdatedAt.Value
            };

            if (_reservationRepository.Add(reservation) > 0)
                return Ok("Reservation was created");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Delete(int id) {
            if (_reservationRepository.Delete(id))
                return Ok("Resevation was deleted");
            else
                return BadRequest("Something went wrong ..");
        }

    }
}
