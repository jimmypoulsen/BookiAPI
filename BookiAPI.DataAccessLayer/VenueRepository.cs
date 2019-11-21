using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer
{
   public class VenueRepository
    {
        public IEnumerable<Venue> Get(int id = 0)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM Venues;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Venue>(SELECT_SQL);
                if (id != 0)
                {
                    return data.Where(v => v.Id == id);
                }
                return data;
            }
        }

        public bool Add(Venue venue)
        {
            const string INSERT_SQL = @"INSERT INTO Venues
                                        (Address, Zip, City)
                                        VALUES (@address, @zip, @city);";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(INSERT_SQL, venue);
                return rows == 1;
            }
        }
    }
}
