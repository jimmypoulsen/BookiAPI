using BookiAPI.DataAccessLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BookiAPI.DataAccessLayer
{
    public class CustomerRepository
    {
        /// <summary>
        /// Gets a list of customers
        /// </summary>
        public IEnumerable<Customer> Get(int id = 0)
        {
            const string SELECT_SQL = @"SELECT *
                                        FROM Customers;";

            using (var conn = Database.Open())
            {
                var data = conn.Query<Customer>(SELECT_SQL);
                if (id != 0)
                {
                    return data.Where(e => e.Id == id);
                }
                return data;
            }
        }

        public bool Add(Customer customer)
        {
            const string INSERT_SQL = @"INSERT INTO Customers
                                        (Name, Phone, Email, Password, CustomerNo)
                                        VALUES (@name, @phone, @email, @password, @customerNo);";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(INSERT_SQL, customer);
                return rows == 1;
            }
        }

        public bool Update(int id, Customer newCustomer)
        {
            IEnumerable<Customer> oldCustomerC = Get(id);
            Customer oldCustomer = oldCustomerC.First<Customer>();

            const string UPDATE_SQL = @"UPDATE Customers
                                        SET Name = @name, Phone = @phone,
                                        Email = @email, Password = @password,
                                        CustomerNo = @customerNo
                                        WHERE Id = @id;";

            Customer updatedCustomer = new Customer
            {
                Name = newCustomer.Name == "" ? oldCustomer.Name : newCustomer.Name,
                Phone = newCustomer.Phone == "" ? oldCustomer.Phone : newCustomer.Phone,
                Email = newCustomer.Email == "" ? oldCustomer.Email : newCustomer.Email,
                Password = newCustomer.Password == "" ? oldCustomer.Password : newCustomer.Password,
                CustomerNo = newCustomer.CustomerNo == 0 ? oldCustomer.CustomerNo : newCustomer.CustomerNo
            };

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(UPDATE_SQL, new
                {
                    updatedCustomer.Name,
                    updatedCustomer.Phone,
                    updatedCustomer.Email,
                    updatedCustomer.Password,
                    updatedCustomer.CustomerNo,
                    id
                });
                return rows == 1;
            }
        }

        public bool Delete(int id)
        {
            const string DELETE_SQL = @"DELETE FROM Customers
                                        WHERE Id = @id;";

            using (var conn = Database.Open())
            {
                var rows = conn.Execute(DELETE_SQL, id);
                return rows == 1;
            }
        }
    }
}
