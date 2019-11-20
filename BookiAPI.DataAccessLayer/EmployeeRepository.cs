using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BookiAPI.DataAccessLayer
{
    public class EmployeeRepository
    {
        /// <summary>
        /// Gets a list of employees
        /// </summary>
        public IEnumerable<Employee> Get(int id = 0)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM Employees;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Employee>(SELECT_SQL);
                if (id != 0)
                {
                    return data.Where(e => e.Id == id);
                }
                return data;
            }
        }

        public bool AddEmployee(Employee employee)
        {
            const string INSERT_SQL = @"INSERT INTO Employees
                                        (Name, Phone, Email, Password, EmployeeNo, Title, AccessLevel)
                                        VALUES (@name, @phone, @email, @password, @employeeNo, @title, @accessLevel);";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(INSERT_SQL, employee);
                return rows == 1;
            }
        }
    }
}
