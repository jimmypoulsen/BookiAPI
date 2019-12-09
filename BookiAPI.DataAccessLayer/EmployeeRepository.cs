using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BookiAPI.DataAccessLayer {
    public class EmployeeRepository {
        /// <summary>
        /// Gets a list of employees
        /// </summary>
        public IEnumerable<Employee> Get(int id = 0) {
            const string SELECT_SQL = @"SELECT *
                                        FROM Employees;";

            using (var conn = Database.Open()) {
                var data = conn.Query<Employee>(SELECT_SQL);
                if (id != 0) {
                    return data.Where(e => e.Id == id);
                }
                return data;
            }
        }

        public IEnumerable<Employee> GetByEmail(string email)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM Employees;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Employee>(SELECT_SQL);
                return data.Where(e => e.Email.Equals(email));
            }
        }

        public IEnumerable<Venue> GetVenuesForEmployee(int id)
        {
            const string SELECT_SQL = @"SELECT
                                            Ven.Id, Ven.Name, Ven.Address, Ven.Zip, Ven.City
                                        FROM
                                            Employees Emp
                                        INNER JOIN
                                            VenueEmployees VenEmp
                                          ON Emp.Id = VenEmp.EmployeeId
                                        INNER JOIN
                                            Venues Ven
                                          ON Ven.Id = VenEmp.VenueId
                                        WHERE Emp.Id = @id";

            using (var conn = Database.Open())
            {
                return conn.Query<Venue>(SELECT_SQL, new { id });
            }
        }

        public int Add(Employee employee) {
            const string INSERT_SQL = @"INSERT INTO Employees
                                        (Name, Phone, Email, Password, EmployeeNo, Title)
                                        output INSERTED.ID
                                        VALUES (@name, @phone, @email, @password, @employeeNo, @title);";

            using (var conn = Database.Open()) {
                int insertedId = (int)conn.ExecuteScalar(INSERT_SQL, employee);
                return insertedId > 0 ? insertedId : 0;
            }
        }
        public bool Delete(int id) {
            const string DELETE_SQL = "DELETE FROM Employees WHERE Id = @id";

            using (var conn = Database.Open()) {
                var rows = conn.Execute(DELETE_SQL, new { id });

                return rows == 1;
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
