using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer
{
    public class VenueRepository {
        public IEnumerable<Venue> Get(int id = 0) {
            const string SELECT_SQL = @"SELECT *
                                        FROM Venues;";

            using (var conn = Database.Open()) {
                var data = conn.Query<Venue>(SELECT_SQL);
                if (id != 0) {
                    return data.Where(v => v.Id == id);
                }
                return data;
            }
        }

        public int Add(Venue venue) {
            const string INSERT_SQL = @"INSERT INTO Venues
                                        (Name, Address, Zip, City)
                                        output INSERTED.ID
                                        VALUES (@name, @address, @zip, @city);";

            using (var conn = Database.Open()) {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, venue);
                return insertedId > 0 ? insertedId : 0;
            }
        }

        public bool Update(int id, Venue newVenue) {
            IEnumerable<Venue> oldVenueV = Get(id);
            Venue oldVenue = oldVenueV.First<Venue>();

            const string UPDATE_SQL = @"UPDATE Venues
                                        SET Name = @name, Address = @address,
                                        Zip = @zip, City = @city,
                                        WHERE Id = @id;";

            Venue updatedVenue = new Venue {
                Name = newVenue.Name == "" ? oldVenue.Name : newVenue.Name,
                Address = newVenue.Address == "" ? oldVenue.Address : newVenue.Address,
                Zip = newVenue.Zip == 0 ? oldVenue.Zip : newVenue.Zip,
                City = newVenue.City == "" ? oldVenue.City : newVenue.City

            };

            using (var conn = Database.Open()) {
                var rows = conn.Execute(UPDATE_SQL, new {
                    updatedVenue.Name,
                    updatedVenue.Address,
                    updatedVenue.Zip,
                    updatedVenue.City,
                    id
                });
                return rows == 1;
            }
        }

        public bool Delete(int id) {
            const string DELETE_SQL = "DELETE FROM Venues WHERE Id = @id";
            using (var conn = Database.Open()) {
                    var rows = conn.Execute(DELETE_SQL, new { id });
                return rows == 1;
            }     
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM Venues";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }
    }
}
