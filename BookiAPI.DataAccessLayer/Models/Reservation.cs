using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models {
    public class Reservation {
        public int Id { get; set; }
        public int ReservationNo { get; set; }
        public string DateTimeStart { get; set; }
        public string DateTimeEnd { get; set; }
        public int State { get; set; }
        public int CustomerId { get; set; }
        public int VenueId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public override string ToString() {
            return $"[{Id}] - {ReservationNo} {State}";
        }
    }
}
