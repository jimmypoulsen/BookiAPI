using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiAPI.RESTfulService.Models
{
    public class VenueResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public IEnumerable<EmployeeResponse> Employees { get; set; }
        public IEnumerable<TablePackageResponse> TablePackages { get; set; }
        public IEnumerable<BeverageResponse> Beverages { get; set; }
        public IEnumerable<VenueHourResponse> VenueHours { get; set; }
        public IEnumerable<TableResponse> Tables { get; set; }
        
    }
}