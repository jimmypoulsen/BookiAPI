using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer
{
    public class ReservationTablePackageRepository
    {

        /// <summary>
        /// Gets a list of reservations
        /// </summary>
        public IEnumerable<ReservationTablePackage> Get(int id = 0)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM ReservationsTablePackages;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<ReservationTablePackage>(SELECT_SQL);
                if (id != 0)
                {
                    return data.Where(r => r.Id == id);
                }
                return data;
            }
        }

        public IEnumerable<ReservationTablePackage> GetByReservationId(int reservationId)
        {
            const string SELECT_SQL = @"SELECT *
                                       FROM ReservationsTablePackages;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<ReservationTablePackage>(SELECT_SQL);
                return data.Where(r => r.ReservationId == reservationId);
            }
        }

        public IEnumerable<TablePackage> GetTablePackagesByReservationId(int reservationId)
        {
            IEnumerable<ReservationTablePackage> rtp = GetByReservationId(reservationId);
            var tablePackageIds = rtp.Select(r => r.TablePackageId).ToList();

            const string SELECT_SQL = @"SELECT *
                                       FROM TablePackages";

            using (var conn = Database.Open())
            {
                var data = conn.Query<TablePackage>(SELECT_SQL);
                return data.Where(tp => tablePackageIds.Contains(tp.Id));
            }
        }

        public int Add(ReservationTablePackage reservationTablePackage)
        {
            const string INSERT_SQL = @"INSERT INTO ReservationsTablePackages
                                        (ReservationId, TablePackageId)
                                        output INSERTED.ID
                                        VALUES (@reservationId, @tablePackageId);";

            using (var conn = Database.Open())
            {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, reservationTablePackage);
                return insertedId > 0 ? insertedId : 0;
            }
        }
        public bool Delete(int id)
        {
            const string DELETE_SQL = "DELETE FROM ReservationsTablePackages WHERE Id = @id";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { id });
                return rows == 1;
            }
        }

        public bool DeleteByReservationId(int reservationId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        ReservationsTablePackages
                                        WHERE ReservationId = @reservationId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { reservationId });
                return rows > 0;
            }
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM ReservationsTablePackages";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }
    }
}
