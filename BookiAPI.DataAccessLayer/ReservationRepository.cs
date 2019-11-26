using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer {
    public class ReservationRepository {

        /// <summary>
        /// Gets a list of reservations
        /// </summary>
        public IEnumerable<Reservation> Get(int id = 0) {
            const string SELECT_SQL = @"SELECT *
                                        FROM Reservations;";

            using (var conn = Database.Open()) {
                var data = conn.Query<Reservation>(SELECT_SQL);
                if (id != 0) {
                    return data.Where(r => r.Id == id);
                }
                return data;
            }
        }

        public bool Add(Reservation reservation) {
            const string INSERT_SQL = @"INSERT INTO Reservations
                                        (ReservationNo, DateTimeStart, DateTimeEnd, State, CustomerId, VenueId, CreatedAt, UpdatedAt)
                                        VALUES (@reservationNo, @dateTimeStart, @dateTimeEnd, @state, @customerId, @venueId, @createdAt, @updatedAt);";

            using (var conn = Database.Open()) {
                var rows = conn.Execute(INSERT_SQL, reservation);
                return rows == 1;
            }
        }

    }
}
