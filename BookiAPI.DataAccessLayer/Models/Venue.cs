﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public int VenueId { get; set; }


        public override string ToString()
        {
            return $"[{Id}] - {Address} {Zip} {City}";
        }

    }
}