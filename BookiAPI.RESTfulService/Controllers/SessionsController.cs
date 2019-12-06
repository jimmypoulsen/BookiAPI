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
    public class SessionsController : ApiController
    {
        private readonly CustomersController _customersController;

        public SessionsController()
        {
            _customersController = new CustomersController();
        }

        // POST /api/sessions/
        public IHttpActionResult Post([FromBody]dynamic data)
        {
            string email = data.Customer.Email.Value;
            string password = data.Customer.Password.Value;
            IEnumerable<CustomerResponse> customers = _customersController.GetByEmail(email);
            CustomerResponse cr;
            if (customers.Any())
            {
                cr = customers.First();
                if (cr.Password.Equals(password))
                    return Ok(cr.Id);
            }
            return Unauthorized();
        }

        public IEnumerable<CustomerResponse> Get()
        {
            return _customersController.GetByEmail("kongen@kongehuset.dk");
        }
    }
}
