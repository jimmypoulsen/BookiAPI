using BookiAPI.DataAccessLayer;
using BookiAPI.RESTfulService.Models;
using BookiAPI.RESTfulService.Helpers;
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
            string email = data.Customer.Email.Value.ToLower();
            string password = data.Customer.Password.Value;
            IEnumerable<CustomerResponse> customers = _customersController.GetByEmail(email);
            CustomerResponse cr;
            if (customers.Any())
            {
                cr = customers.First();
                string expectedPw = HashingHelper.GenerateHash(password, cr.Salt);
                string actualPw = cr.Password;
                if (expectedPw.Equals(actualPw))
                    return Ok(cr.Id);
            }
            return Unauthorized();
        }
    }
}
