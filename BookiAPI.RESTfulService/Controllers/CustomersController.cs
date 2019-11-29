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
    public class CustomersController : ApiController
    {
        private readonly CustomerRepository _customerRepository;

        public CustomersController()
        {
            _customerRepository = new CustomerRepository();
        }

        // GET /api/customers/
        public IEnumerable<CustomerResponse> Get()
        {
            return _customerRepository.Get().Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Password = customer.Password,
                CustomerNo = customer.CustomerNo
            });
        }

        // GET /api/customers/1/
        public IEnumerable<CustomerResponse> Get(int id)
        {
            return _customerRepository.Get(id).Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Password = customer.Phone,
                CustomerNo = customer.CustomerNo
            });
        }

        public IEnumerable<CustomerResponse> GetByEmail(string email)
        {
            return _customerRepository.GetByEmail(email).Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Password = customer.Password,
                CustomerNo = customer.CustomerNo
            });
        }

        public IHttpActionResult Post([FromBody]dynamic data)
        {
            BookiAPI.DataAccessLayer.Models.Customer customer = new DataAccessLayer.Models.Customer
            {
                Name = data.Customer.Name.Value,
                Phone = data.Customer.Phone.Value,
                Email = data.Customer.Email.Value,
                Password = data.Customer.Password.Value,
                CustomerNo = (int)data.Customer.CustomerNo.Value
            };

            if (_customerRepository.Add(customer) > 0)
                return Ok("Customer was created");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Put([FromBody]dynamic data)
        {
            int id = (int)data.Id.Value;
            BookiAPI.DataAccessLayer.Models.Customer newCustomer = new BookiAPI.DataAccessLayer.Models.Customer
            {
                Name = data.Customer.Name.Value == null ? "" : data.Customer.Name.Value,
                Phone = data.Customer.Phone.Value == null ? "" : data.Customer.Phone.Value,
                Email = data.Customer.Email.Value == null ? "" : data.Customer.Email.Value,
                Password = data.Customer.Password.Value == null ? "" : data.Customer.Password.Value,
                CustomerNo = data.Customer.CustomerNo.Value == null ? 0 : (int)data.Customer.CustomerNo.Value
            };

            if (_customerRepository.Update(id, newCustomer))
                return Ok($"Customer with ID: {id} was updated");
            else
                return BadRequest("Something went wrong ..");
        }

        public IHttpActionResult Delete(int id) {
            if (_customerRepository.Delete(id))
                return Ok("Customer was deleted");
            else
                return BadRequest("Something went wrong ..");
        }
    }
}
