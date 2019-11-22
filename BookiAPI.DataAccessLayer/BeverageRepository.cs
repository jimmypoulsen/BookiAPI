using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Add(Beverage beverage) {
            const string INSERT_SQL = @"INSERT INTO Beverages
                                        (Name, Barcode, Description, CostPrice, SalesPrice, Stock, VenueId)
                                        VALUES (@name, @barcode, @description, @costPrice, @salesPrice, @stock, @venueId);";
            // To Do: Change venueID
            using (var conn = Database.Open()) {
                var rows = conn.Execute(INSERT_SQL, beverage);
                return rows == 1;
            }
        }

    }


}
