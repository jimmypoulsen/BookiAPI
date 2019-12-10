using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookiAPI.RESTfulService.Controllers
{
    public class ReservationsController : ApiController
    {

        private readonly ReservationRepository _reservationRepository;
        private readonly TablesController _tablesController;
        private readonly TablePackagesController _tablePackagesController;
        private readonly ReservationsTablePackagesController _reservationsTablePackagesController;

        public ReservationsController() {
            _reservationRepository = new ReservationRepository();
            _tablesController = new TablesController();
            _tablePackagesController = new TablePackagesController();
            _reservationsTablePackagesController = new ReservationsTablePackagesController();
        }

        // GET /api/employees/
        public IEnumerable<ReservationResponse> Get() {
            return _reservationRepository.Get().Select(reservation => new ReservationResponse {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                DateTimeStart = reservation.DateTimeStart.ToString(),
                DateTimeEnd = reservation.DateTimeEnd.ToString(),
                State = reservation.State,
                CustomerId = reservation.CustomerId,
                VenueId = reservation.VenueId,
                TableId = reservation.TableId,
                CreatedAt = reservation.CreatedAt.ToString(),
                UpdatedAt = reservation.UpdatedAt.ToString(),
                Table = _tablesController.Get(reservation.TableId),
                TablePackages = _reservationsTablePackagesController.GetTablePackagesByReservationId(reservation.Id)
            });
        }

        // GET /api/employees/1/
        public IEnumerable<ReservationResponse> Get(int id) {
            return _reservationRepository.Get(id).Select(reservation => new ReservationResponse {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                DateTimeStart = reservation.DateTimeStart.ToString(),
                DateTimeEnd = reservation.DateTimeEnd.ToString(),
                State = reservation.State,
                CustomerId = reservation.CustomerId,
                VenueId = reservation.VenueId,
                TableId = reservation.TableId,
                CreatedAt = reservation.CreatedAt.ToString(),
                UpdatedAt = reservation.UpdatedAt.ToString(),
                Table = _tablesController.Get(reservation.TableId),
                TablePackages = _reservationsTablePackagesController.GetTablePackagesByReservationId(reservation.Id)
            });
        }

        public IEnumerable<ReservationResponse> GetByCustomer(int customerId)
        {
            
            return _reservationRepository.GetByCustomer(customerId).Select(reservation => new ReservationResponse
            {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                DateTimeStart = reservation.DateTimeStart.ToString(),
                DateTimeEnd = reservation.DateTimeEnd.ToString(),
                State = reservation.State,
                CustomerId = reservation.CustomerId,
                VenueId = reservation.VenueId,
                TableId = reservation.TableId,
                CreatedAt = reservation.CreatedAt.ToString(),
                UpdatedAt = reservation.UpdatedAt.ToString(),
                Table = _tablesController.Get(reservation.TableId),
                TablePackages = _reservationsTablePackagesController.GetTablePackagesByReservationId(reservation.Id)
            });
        }

        public IEnumerable<ReservationResponse> GetByVenueId(int venueId)
        {

            return _reservationRepository.GetByVenueId(venueId).Select(reservation => new ReservationResponse
            {
                Id = reservation.Id,
                ReservationNo = reservation.ReservationNo,
                DateTimeStart = reservation.DateTimeStart.ToString(),
                DateTimeEnd = reservation.DateTimeEnd.ToString(),
                State = reservation.State,
                CustomerId = reservation.CustomerId,
                VenueId = reservation.VenueId,
                TableId = reservation.TableId,
                CreatedAt = reservation.CreatedAt.ToString(),
                UpdatedAt = reservation.UpdatedAt.ToString(),
                Table = _tablesController.Get(reservation.TableId),
                TablePackages = _reservationsTablePackagesController.GetTablePackagesByReservationId(reservation.Id)
            });
        }

        // POST /api/employees/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data) {
            Random random = new Random();
            BookiAPI.DataAccessLayer.Models.Reservation reservation = new DataAccessLayer.Models.Reservation {
                ReservationNo = random.Next(1000000),
                DateTimeStart = Convert.ToDateTime(data.Reservation.DateTimeStart.Value),
                DateTimeEnd = Convert.ToDateTime(data.Reservation.DateTimeEnd.Value),
                State = 1,
                CustomerId = (int) data.Reservation.CustomerId.Value,
                VenueId = (int) data.Reservation.VenueId.Value,
                TableId = (int) data.Reservation.TableId.Value,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            if (IsTableAvailable(reservation.TableId, reservation.DateTimeStart, reservation.DateTimeEnd)) {
                int reservationId = _reservationRepository.Add(reservation);

                if (reservationId > 0)
                    return Ok("" + reservationId);
                else
                    return BadRequest("Something went wrong ..");
            }
            else
                return BadRequest("That table is already booked!");
        }

        public IHttpActionResult Delete(int id) {
            if (_reservationsTablePackagesController.DeleteByReservationId(id))
            {
                if (_reservationRepository.Delete(id))
                    return Ok("Resevation was deleted");
                else
                    return BadRequest("Something went wrong ..");
            }
            else
                return BadRequest("Something went wrong ..");

        }

        public bool DeleteByVenueId(int venueId)
        {
            IEnumerable<ReservationResponse> reservations = GetByVenueId(venueId);
            foreach (ReservationResponse res in reservations) {
                _reservationsTablePackagesController.DeleteByReservationId(res.Id);
            }

            if (_reservationRepository.DeleteByVenueId(venueId))
                return true;
            else
                return false;
        }

        private bool IsTableAvailable(int tableId, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            Debug.WriteLine(dateTimeStart.ToString());
            Debug.WriteLine(dateTimeEnd.ToString());

            return _reservationRepository.IsTableAvailable(tableId, dateTimeStart.ToString(), dateTimeEnd.ToString());
        }
    }
}
