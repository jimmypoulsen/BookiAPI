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
    public class VenueEmployeesController : ApiController
    {
        private readonly VenueEmployeeRepository _venueEmployeeRepository;

        public VenueEmployeesController()
        {
            _venueEmployeeRepository = new VenueEmployeeRepository();
        }

        // GET /api/venue/
        public IEnumerable<VenueEmployeeResponse> Get()
        {
            return _venueEmployeeRepository.Get().Select(venueEmployee => new VenueEmployeeResponse
            {
                Id = venueEmployee.Id,
                EmployeeId = venueEmployee.EmployeeId,
                VenueId = venueEmployee.VenueId,
                AccessLevel = venueEmployee.AccessLevel
            });
        }

        // GET /api/venue/1/
        public IEnumerable<VenueEmployeeResponse> Get(int id)
        {
            return _venueEmployeeRepository.Get(id).Select(venueEmployee => new VenueEmployeeResponse
            {
                Id = venueEmployee.Id,
                EmployeeId = venueEmployee.EmployeeId,
                VenueId = venueEmployee.VenueId,
                AccessLevel = venueEmployee.AccessLevel
            });
        }

        public IEnumerable<VenueEmployeeResponse> GetByEmployeeId(int employeeId)
        {
            return _venueEmployeeRepository.GetByEmployeeId(employeeId).Select(venueEmployee => new VenueEmployeeResponse
            {
                Id = venueEmployee.Id,
                EmployeeId = venueEmployee.EmployeeId,
                VenueId = venueEmployee.VenueId,
                AccessLevel = venueEmployee.AccessLevel
            });
        }
    }

}