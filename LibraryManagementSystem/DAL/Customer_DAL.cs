using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using System.Net;

namespace LibraryManagementSystem.DAL
{
    public class Customer_DAL
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customerList = new List<Customer>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetAllCustomers";

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    customerList.Add(new Customer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Code = dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Address = dr["Address"].ToString()
                    });
                }
                _connection.Close();
            }
            return customerList;
        }

        public bool AddCustomer(Customer customer)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_AddCustomer";

                _command.Parameters.AddWithValue("@Code", customer.Code);
                _command.Parameters.AddWithValue("@Name", customer.Name);
                _command.Parameters.AddWithValue("@Email", customer.Email);
                _command.Parameters.AddWithValue("@Phone", customer.Phone);
                _command.Parameters.AddWithValue("@Address", customer.Address);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = new Customer();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetCustomerById";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    customer.Id = Convert.ToInt32(dr["Id"]);
                    customer.Code = dr["Code"].ToString();
                    customer.Name = dr["Name"].ToString();
                    customer.Email = dr["Email"].ToString();
                    customer.Phone = dr["Phone"].ToString();
                    customer.Address = dr["Address"].ToString();
                }
                _connection.Close();
            }
            return customer;
        }

        public bool UpdateCustomer(Customer customer)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_UpdateCustomer";

                _command.Parameters.AddWithValue("@Id", customer.Id);
                _command.Parameters.AddWithValue("@Code", customer.Code);
                _command.Parameters.AddWithValue("@Name", customer.Name);
                _command.Parameters.AddWithValue("@Email", customer.Email);
                _command.Parameters.AddWithValue("@Phone", customer.Phone);
                _command.Parameters.AddWithValue("@Address", customer.Address);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public bool DeleteCustomer(int id)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_DeleteCustomer";

                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public List<Customer> GetCustomerByCode(string code)
        {
            List<Customer> customerList = new List<Customer>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetCustomerByCode";
                _command.Parameters.AddWithValue("@Code", code);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    customerList.Add(new Customer
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Code = dr["Code"].ToString(),
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Address = dr["Address"].ToString()
                    });
                }
                _connection.Close();
            }
            return customerList;
        }

        public string GetLastCustomerCode()
        {

            string? lastCustomerCode = null;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetLastCustomerCode";

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    lastCustomerCode = dr["Code"].ToString();
                }
                _connection.Close();
            }
            return lastCustomerCode;
        }
    }
}
