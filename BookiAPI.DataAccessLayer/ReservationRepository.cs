﻿using BookiAPI.DataAccessLayer.Models;
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

        public IEnumerable<Reservation> GetByCustomer(int customerId)
        {
            const string SELECT_SQL = @"SELECT *
                                       FROM Reservations;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Reservation>(SELECT_SQL);
                return data.Where(r => r.CustomerId == customerId);
            }
        }

        public IEnumerable<Reservation> GetByVenueId(int venueId)
        {
            const string SELECT_SQL = @"SELECT *
                                       FROM Reservations;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Reservation>(SELECT_SQL);
                return data.Where(r => r.VenueId == venueId);
            }
        }

        public bool IsTableAvailable(int tableId, string dateTimeStart, string dateTimeEnd)
        {
            const string SELECT_SQL = @"SELECT * FROM Reservations
                                        WHERE TableId = @tableId AND
                                        DateTimeEnd >= CONVERT(DATETIME, @dateTimeStart, 105) AND
                                        DateTimeStart <= CONVERT(DATETIME, @dateTimeEnd, 105)";

            var parameters = new DynamicParameters();
            parameters.Add("@tableId", tableId);
            parameters.Add("@dateTimeStart", dateTimeStart);
            parameters.Add("@dateTimeEnd", dateTimeEnd);

            using (var conn = Database.Open())
            {
                var data = conn.Query<Reservation>(SELECT_SQL, parameters);
                return !data.Any(); // return false if there is any reservations
            }
        }

        public int Add(Reservation reservation) {
            const string INSERT_SQL = @"INSERT INTO Reservations
                                        (ReservationNo, DateTimeStart, DateTimeEnd, State, CustomerId, VenueId, TableId, CreatedAt, UpdatedAt)
                                        output INSERTED.ID
                                        VALUES (@reservationNo, @dateTimeStart, @dateTimeEnd, @state, @customerId, @venueId, @tableId, @createdAt, @updatedAt);";

            using (var conn = Database.Open()) {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, reservation);
                return insertedId > 0 ? insertedId : 0;
            }
        }
        public bool Delete(int id) {
            const string DELETE_SQL = "DELETE FROM Reservations WHERE Id = @id";

            using (var conn = Database.Open()) {
                var rows = conn.Execute(DELETE_SQL, new { id });

                return rows == 1;
            }
        }

        public bool DeleteByVenueId(int venueId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        Reservations WHERE
                                        VenueId = @venueId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { venueId });
                return rows > 0;
            }
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM Reservations";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }
    }
}
