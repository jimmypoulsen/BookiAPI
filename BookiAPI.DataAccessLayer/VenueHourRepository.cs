using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer {
    public class VenueHourRepository {
        public IEnumerable<VenueHour> Get(int id = 0) {
            const string SELECT_SQL = @"SELECT *
                                        FROM VenueHours;";

            using (var conn = Database.Open()) {
                var data = conn.Query<VenueHour>(SELECT_SQL);
                if (id != 0) {
                    return data.Where(v => v.Id == id);
                }
                return data;
            }
        }

        public IEnumerable<VenueHour> GetByVenueId(int venueId) {
            const string SELECT_SQL = @"SELECT *
                                        FROM VenueHours;";

            using (var conn = Database.Open()) {
                var data = conn.Query<VenueHour>(SELECT_SQL);
                return data.Where(v => v.VenueId == venueId);
            }
        }

        public int Add(VenueHour venueHour) {
            const string INSERT_SQL = @"INSERT INTO VenueHours
                                        (WeekDay, OpenTime, CloseTime, VenueId)
                                        output INSERTED.ID
                                        VALUES (@weekDay, @openTime, @closeTime, @venueId);";

            using (var conn = Database.Open()) {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, venueHour);
                return insertedId > 0 ? insertedId : 0;
            }

        }
        public bool Delete(int id) {
            const string DELETE_SQL = "DELETE FROM VenueHours WHERE Id = @id";

            using (var conn = Database.Open()) {
                var rows = conn.Execute(DELETE_SQL, new { id });
                return rows == 1;
            }
        }

        public bool DeleteByVenueId(int venueId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        VenueHours WHERE
                                        VenueId = @venueId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { venueId });
                return rows > 0;
            }
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM VenueHours";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }
    }
}
