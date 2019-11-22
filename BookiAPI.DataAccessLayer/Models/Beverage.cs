using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models {
    public class Beverage {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int CostPrice { get; set; }
        public int SalesPrice { get; set; }
        public int Stock { get; set; }
        public int VenueId { get; set; }

        public override string ToString() {
            return $"[{Id}] - {Name} {Barcode}";
        }

    }
}
