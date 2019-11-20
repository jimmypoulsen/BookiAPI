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
    public class EmployeesController : ApiController
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeesController()
        {
            _employeeRepository = new EmployeeRepository();
        }

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
                AccessLevel = employee.AccessLevel
            });
        }

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
                AccessLevel = employee.AccessLevel
            });
        }

        public IHttpActionResult Post([FromBody]dynamic data)
        {
            BookiAPI.DataAccessLayer.Models.Employee employee = new DataAccessLayer.Models.Employee
            {
                Name = data.Employee.Name.Value,
                Phone = data.Employee.Phone.Value,
                Email = data.Employee.Email.Value,
                Password = data.Employee.Password.Value,
                EmployeeNo = (int)data.Employee.EmployeeNo.Value,
                Title = data.Employee.Title.Value,
                AccessLevel = (int)data.Employee.AccessLevel.Value
            };

            if (_employeeRepository.AddEmployee(employee))
                return Ok("Employee was created");
            else
                return BadRequest("Something went wrong ..");
        }
    }
}
