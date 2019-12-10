using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer
{
    public class TableRepository
    {
        public IEnumerable<Table> Get(int id = 0)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM Tables;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Table>(SELECT_SQL);
                if (id != 0)
                {
                    return data.Where(t => t.Id == id);
                }
                return data;
            }
        }

        public IEnumerable<Table> GetByVenueId(int venueId)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM Tables;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Table>(SELECT_SQL);
                return data.Where(t => t.VenueId == venueId);
            }
        }

        public IEnumerable<Table> GetAvailableTables(int venueId, string dateTimeStart, string dateTimeEnd)
        {
            const string SELECT_SQL = @"SELECT Tabel.Id AS Id, Tabel.Name AS Name, Tabel.NoOfSeats as NoOfSeats, Tabel.VenueId AS VenueId
	                                       FROM Tables AS Tabel
                                        WHERE NOT EXISTS(
                                            SELECT * FROM Reservations
                                            WHERE VenueId = @venueId AND TableId = Tabel.Id AND
                                            DateTimeEnd >= CONVERT(DATETIME, @dateTimeStart, 105) AND
                                            DateTimeStart <= CONVERT(DATETIME, @dateTimeEnd, 105)
                                        );";

            var parameters = new DynamicParameters();
            parameters.Add("@venueId", venueId);
            parameters.Add("@dateTimeStart", dateTimeStart);
            parameters.Add("@dateTimeEnd", dateTimeEnd);

            using (var conn = Database.Open())
            {
                var data = conn.Query<Table>(SELECT_SQL, parameters);
                return data;
            }
        }

        public int Add(Table table)
        {
            const string INSERT_SQL = @"INSERT INTO Tables
                                        (NoOfSeats, Name, VenueId)
                                        output INSERTED.ID
                                        VALUES (@noOfSeats, @name, @venueId);";

            using (var conn = Database.Open())
            {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, table);
                return insertedId > 0 ? insertedId : 0;
            }
        }
        public bool Delete(int id)
        {
            const string DELETE_SQL = "DELETE FROM Tables WHERE Id = @id";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { id });
                return rows == 1;
            }
        }

        public bool DeleteByVenueId(int venueId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        Tables WHERE
                                        VenueId = @venueId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { venueId });
                return rows > 0;
            }
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM Tables";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }
    }
}
