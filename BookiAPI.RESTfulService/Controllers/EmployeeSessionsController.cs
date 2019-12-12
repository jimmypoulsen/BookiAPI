﻿using BookiAPI.DataAccessLayer;
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
    public class EmployeeSessionsController : ApiController
    {
        private readonly EmployeesController _employeesController;

        public EmployeeSessionsController()
        {
            _employeesController = new EmployeesController();
        }

        public IHttpActionResult Post([FromBody]dynamic data)
        {
            string email = data.Employee.Email.Value.ToLower();
            string password = data.Employee.Password.Value;
            IEnumerable<EmployeeResponse> employees = _employeesController.GetByEmail(email);
            EmployeeResponse er;
            if (employees.Any())
            {
                er = employees.First();
                string expectedPw = er.Password + er.Salt;
                string actualPw = password + er.Salt;
                if (expectedPw.Equals(actualPw))
                    return Ok(er.Id);
            }
            return Unauthorized();
        }
    }
}
