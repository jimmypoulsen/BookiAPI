using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models
{
    public class VenueHour
    {
        public int Id { get; set; }
        public string WeekDay { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public int VenueId { get; set; }


        public override string ToString()
        {
            return $"[{Id}] - {OpenTime} {CloseTime}";
        }

    }
}
