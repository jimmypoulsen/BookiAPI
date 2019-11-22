using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models {
    public class TablePackage {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int VenueId { get; set; }

        public override string ToString() {
            return $"[{Id}] - {Name} {Price} {VenueId}";
        }

    }
}
