using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiAPI.RESTfulService.Models
{
    public class ReservationTablePackageResponse
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int TablePackageId { get; set; }

    }
}