using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace BookiAPI.DataAccessLayer.Models
{
    public class VenueEmployee
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public int EmployeeId { get; set; }
        public int AccessLevel { get; set; }
    }
}
