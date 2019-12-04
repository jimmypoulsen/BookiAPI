using BookiAPI.DataAccessLayer;
using BookiAPI.DataAccessLayer.Models;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookiAPI.RESTfulService.Controllers
{
    public class ReservationsTablePackagesController : ApiController
    {

        private readonly ReservationTablePackageRepository _reservationTablePackageRepository;

        public ReservationsTablePackagesController()
        {
            _reservationTablePackageRepository = new ReservationTablePackageRepository();
        }

        // GET /api/employees/
        public IEnumerable<ReservationTablePackageResponse> Get()
        {
            return _reservationTablePackageRepository.Get().Select(reservationTablePackage => new ReservationTablePackageResponse
            {
                Id = reservationTablePackage.Id,
                ReservationId = reservationTablePackage.ReservationId,
                TablePackageId = reservationTablePackage.TablePackageId
            });
        }

        // GET /api/employees/1/
        public IEnumerable<ReservationTablePackageResponse> Get(int id)
        {
            return _reservationTablePackageRepository.Get(id).Select(reservationTablePackage => new ReservationTablePackageResponse
            {
                Id = reservationTablePackage.Id,
                ReservationId = reservationTablePackage.ReservationId,
                TablePackageId = reservationTablePackage.TablePackageId
            });
        }

        public IEnumerable<ReservationTablePackageResponse> GetByReservationId(int reservationId)
        {
            return _reservationTablePackageRepository.GetByReservationId(reservationId).Select(reservationTablePackage => new ReservationTablePackageResponse
            {
                Id = reservationTablePackage.Id,
                ReservationId = reservationTablePackage.ReservationId,
                TablePackageId = reservationTablePackage.TablePackageId
            });
        }

        public IEnumerable<TablePackageResponse> GetTablePackagesByReservationId(int reservationId)
        {
            return _reservationTablePackageRepository.GetTablePackagesByReservationId(reservationId).Select(tablePackage => new TablePackageResponse
            {
                Id = tablePackage.Id,
                Name = tablePackage.Name,
                Price = tablePackage.Price,
                VenueId = tablePackage.VenueId
            });
        }

        // POST /api/employees/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data)
        {
            BookiAPI.DataAccessLayer.Models.ReservationTablePackage reservationTablePackage = new DataAccessLayer.Models.ReservationTablePackage
            {
                ReservationId = Convert.ToInt32(data.ReservationTablePackage.ReservationId.Value),
                TablePackageId = Convert.ToInt32(data.ReservationTablePackage.TablePackageId.Value)
            };

            if (_reservationTablePackageRepository.Add(reservationTablePackage) > 0)
                return Ok("ReservationTablePackage was created");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Delete(int id)
        {
            if (_reservationTablePackageRepository.Delete(id))
                return Ok("ReservationTablePackage was deleted");
            else
                return BadRequest("Something went wrong ..");
        }

        public bool DeleteByReservationId(int reservationId)
        {
            return _reservationTablePackageRepository.DeleteByReservationId(reservationId);
        }
    }
}
