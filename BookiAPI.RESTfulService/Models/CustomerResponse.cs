using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiAPI.RESTfulService.Models
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CustomerNo { get; set; }
        public string Salt { get; set; }
        public IEnumerable<ReservationResponse> Reservations { get; set; }
    }
}
