using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiAPI.RESTfulService.Models {
    public class ReservationResponse {
        public int Id { get; set; }
        public int ReservationNo { get; set; }
        public string DateTimeStart { get; set; }
        public string DateTimeEnd { get; set; }
        public int State { get; set; }
        public int CustomerId { get; set; }
        public int VenueId { get; set; }
        public int TableId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public IEnumerable<TableResponse> Table { get; set; }
        public IEnumerable<TablePackageResponse> TablePackages { get; set; }

    }
}