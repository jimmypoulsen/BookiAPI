using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models {
    public class Reservation {
        public int Id { get; set; }
        public int ReservationNo { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public int State { get; set; }
        public int CustomerId { get; set; }
        public int VenueId { get; set; }
        public int TableId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString() {
            return $"[{Id}] - {ReservationNo} {State}";
        }
    }
}
