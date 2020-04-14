using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace BookiAPI.DataAccessLayer.Models
{
    /// <summary>
    /// A class representing an employee
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CustomerNo { get; set; }
        public string Salt { get; set; }
        public string FacebookUserID { get; set; }
        public string GoogleUserID { get; set; }

        public override string ToString()
        {
            return $"[{Id}] - {Name} {Email}";
        }
    }
}
