using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookiAPI.RESTfulService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly VenuesController _venuesController;
        private readonly VenueEmployeesController _venueEmployeesController;

        public EmployeesController()
        {
            _employeeRepository = new EmployeeRepository();
            _venuesController = new VenuesController();
            _venueEmployeesController = new VenueEmployeesController();
        }
        
        // GET /api/employees/
        public IEnumerable<EmployeeResponse> Get()
        {
            return _employeeRepository.Get().Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Phone = employee.Phone,
                Email = employee.Email,
                Password = employee.Password,
                EmployeeNo = employee.EmployeeNo,
                Title = employee.Title,
                Venues = GetVenuesForEmployee(employee.Id)
            });
        }

        // GET /api/employees/1/
        public IEnumerable<EmployeeResponse> Get(int id)
        {
            return _employeeRepository.Get(id).Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Phone = employee.Phone,
                Email = employee.Email,
                Password = employee.Password,
                EmployeeNo = employee.EmployeeNo,
                Title = employee.Title,
                Venues = GetVenuesForEmployee(employee.Id)
            });
        }

        public IEnumerable<EmployeeResponse> GetByEmail(string email)
        {
            return _employeeRepository.GetByEmail(email).Select(employee => new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Phone = employee.Phone,
                Email = employee.Email,
                Password = employee.Password,
                EmployeeNo = employee.EmployeeNo,
                Title = employee.Title,
                Venues = GetVenuesForEmployee(employee.Id)
            });
        }

        private IEnumerable<VenueResponse> GetVenuesForEmployee(int id)
        {
            return _employeeRepository.GetVenuesForEmployee(id).Select(venue => new VenueResponse
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address,
                City = venue.City,
                Zip = venue.Zip
            });
        }

        // POST /api/employees/
        // body: JSON
        public IHttpActionResult Post([FromBody]dynamic data)
        {
            int venueId = (int)data.Venue.Id.Value;
            BookiAPI.DataAccessLayer.Models.Employee employee = new DataAccessLayer.Models.Employee
            {
                Name = data.Employee.Name.Value,
                Phone = data.Employee.Phone.Value,
                Email = data.Employee.Email.Value.ToLower(),
                Password = data.Employee.Password.Value,
                EmployeeNo = (int)data.Employee.EmployeeNo.Value,
                Title = data.Employee.Title.Value,
                Salt = data.Employee.Salt.Value
            };

            int employeeId = _employeeRepository.Add(employee);

            if (employeeId > 0)
            {
                if (_venueEmployeesController.Post(venueId, employeeId))
                    return Ok("Employee was created");
            }
            return BadRequest("Something went wrong ..");
        }
        public IHttpActionResult Delete(int id) {

            // delete venueEmployees

            _venueEmployeesController.DeleteByEmployeeId(id);

            if (_employeeRepository.Delete(id))
                return Ok("Employee was deleted");
            else
                return BadRequest("Something went wrong ..");
        }
    }
}
