using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BookiAPI.DataAccessLayer
{
    public class VenueEmployeeRepository
    {
        public IEnumerable<VenueEmployee> Get(int id = 0)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM VenueEmployees;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<VenueEmployee>(SELECT_SQL);
                if (id != 0)
                {
                    return data.Where(ve => ve.Id == id);
                }
                return data;
            }
        }

        public IEnumerable<VenueEmployee> GetByEmployeeId(int employeeId)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM VenueEmployees;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<VenueEmployee>(SELECT_SQL);
                return data.Where(ve => ve.EmployeeId == employeeId);
            }
        }

        public int Add(VenueEmployee venueEmployee)
        {
            const string INSERT_SQL = @"INSERT INTO VenueEmployees
                                        (VenueId, EmployeeId, AccessLevel)
                                        output INSERTED.ID
                                        VALUES (@venueId, @employeeId, @accessLevel);";

            using (var conn = Database.Open())
            {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, venueEmployee);
                return insertedId > 0 ? insertedId : 0;
            }
        }
        public bool Delete(int id)
        {
            const string DELETE_SQL = "DELETE FROM VenueEmployees WHERE Id = @id";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { id });

                return rows == 1;
            }
        }

        public bool DeleteByEmployeeId(int employeeId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        VenueEmployees
                                        WHERE
                                        EmployeeId = @employeeId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { employeeId });
                return rows > 0;
            }
        }

        public bool DeleteByVenueId(int venueId)
        {
            const string DELETE_SQL = @"DELETE FROM
                                        VenueEmployees
                                        WHERE
                                        VenueId = @venueId;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, new { venueId });
                return rows > 0;
            }
        }

        public bool Truncate()
        {
            const string DELETE_SQL = "DELETE FROM Employees";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL);
                return rows > 1;
            }
        }
    }
}
