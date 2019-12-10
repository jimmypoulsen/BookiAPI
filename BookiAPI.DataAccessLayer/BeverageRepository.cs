using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BookiAPI.DataAccessLayer {
    public class BeverageRepository {

        /// <summary>
        /// Gets a list of beverages
        /// </summary>
        public IEnumerable<Beverage> Get(int id = 0) {
            const string SELECT_SQL = @"SELECT *
                                        FROM Beverages;";

            using (var conn = Database.Open()) {
                var data = conn.Query<Beverage>(SELECT_SQL);
                if (id != 0) {
                    return data.Where(b => b.Id == id);
                }
                return data;
            }
        }

        public IEnumerable<Beverage> GetByVenueId(int venueId) {
            const string SELECT_SQL = @"SELECT * FROM
                                        Beverages;";

            using (var conn = Database.Open()) {
                var data = conn.Query<Beverage>(SELECT_SQL);
                return data.Where(b => b.VenueId == venueId);
            }
        }

        public int Add(Beverage beverage) {
            const string INSERT_SQL = @"INSERT INTO Beverages
                                        (Name, Barcode, Description, CostPrice, SalesPrice, Stock, VenueId)
                                        output INSERTED.ID
                                        VALUES (@name, @barcode, @description, @costPrice, @salesPrice, @stock, @venueId);";
            // To Do: Change venueID
            using (var conn = Database.Open()) {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, beverage);
                return insertedId > 0 ? insertedId : 0;
            }
        }

        public bool Delete(int id) {
            const string DELETE_SQL = "DELETE FROM Beverages WHERE Id = @id";

            using (var conn = Database.Open()) {
                var rows = conn.Execute(DELETE_SQL, new { id });
                return rows == 1;
            }
        }

        public bool DeleteByVenueId(int venueId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        Beverages WHERE
                                        VenueId = @venueId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { venueId });
                return rows > 0;
            }
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM Beverages";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }

    }
}
