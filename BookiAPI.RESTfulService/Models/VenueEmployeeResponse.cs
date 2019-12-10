using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.RESTfulService.Models
{
    public class VenueEmployeeResponse
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public int EmployeeId { get; set; }
        public int AccessLevel { get; set; }
    }
}
