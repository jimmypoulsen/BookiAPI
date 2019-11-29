using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer {
    public class TablePackageRepository {

        /// <summary>
        /// Gets a list of tablePackages
        /// </summary>
        public IEnumerable<TablePackage> Get(int id = 0) {
            const string SELECT_SQL = @"SELECT *
                                        FROM TablePackages;";

            using (var conn = Database.Open()) {
                var data = conn.Query<TablePackage>(SELECT_SQL);
                if (id != 0) {
                    return data.Where(tp => tp.Id == id);
                }
                return data;
            }
        }

        public int Add(TablePackage tablePackage) {
            const string INSERT_SQL = @"INSERT INTO TablePackages
                                        (Name, Price, VenueId)
                                        output INSERTED.ID
                                        VALUES (@name, @price, @venueId);";

            using (var conn = Database.Open()) {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, tablePackage);
                return insertedId > 0 ? insertedId : 0;
            }
        }

        public bool Delete(int id) {
            const string DELETE_SQL = "DELETE FROM TablePackages WHERE Id = @id";

            using (var conn = Database.Open()) {
                var rows = conn.Execute(DELETE_SQL, new { id });

                return rows == 1;
            }
        }

    }
}
