using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.RESTfulService.Models
{
    public class TableResponse
    {
        public int Id { get; set; }
        public int NoOfSeats { get; set; }
        public string Name { get; set; }
        public int VenueId { get; set; }
    }
}
