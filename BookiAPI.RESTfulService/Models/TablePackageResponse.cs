using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiAPI.RESTfulService.Models {
    public class TablePackageResponse {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int VenueId { get; set; }

    }
}