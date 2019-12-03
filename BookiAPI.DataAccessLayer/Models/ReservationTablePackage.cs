using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models
{
    public class ReservationTablePackage
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int TablePackageId { get; set; }

    }
}
