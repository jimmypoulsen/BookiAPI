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

        public bool Post(int venueId, int employeeId, int accessLevel = 1) 
        {
            BookiAPI.DataAccessLayer.Models.VenueEmployee venueEmployee = new DataAccessLayer.Models.VenueEmployee
            {
                VenueId = venueId,
                EmployeeId = employeeId,
                AccessLevel = accessLevel
            };

            return _venueEmployeeRepository.Add(venueEmployee) > 0;
        }

        public bool DeleteByVenueId(int venueId) {
            return _venueEmployeeRepository.DeleteByVenueId(venueId);
        }

        public bool DeleteByEmployeeId(int employeeId) {
            return _venueEmployeeRepository.DeleteByEmployeeId(employeeId);
        }
    }

}