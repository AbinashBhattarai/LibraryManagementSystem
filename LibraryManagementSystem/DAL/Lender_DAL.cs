using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem.DAL
{
    public class Lender_DAL
    {
        public List<LenderViewModel> GetAllLenders()
        {
            List<LenderViewModel> lenderList = new List<LenderViewModel>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetAllLenders";

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    lenderList.Add(new LenderViewModel
                    {
                        CustomerCode = dr["Code"].ToString(),
                        CustomerName = dr["Name"].ToString(),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        BookId = Convert.ToInt32(dr["BookId"]),
                        CustomerId = Convert.ToInt32(dr["CustomerId"]),
                        IssueDate = Convert.ToDateTime(dr["IssueDate"]),
                        ReturnDate = Convert.ToDateTime(dr["ReturnDate"]),
                        Penalty = Convert.ToInt32(dr["Penalty"]),
                    });
                }
                _connection.Close();
            }
            return lenderList;
        }

        public List<LenderViewModel> GetLenderByCode(string code)
        {
            List<LenderViewModel> lenderList = new List<LenderViewModel>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetLenderByCode";
                _command.Parameters.AddWithValue("@Code", code);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    lenderList.Add(new LenderViewModel
                    {
                        CustomerCode = dr["Code"].ToString(),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        Penalty = Convert.ToInt32(dr["Penalty"]),
                    });
                }
                _connection.Close();
            }
            return lenderList;
        }

        public bool AddLender(Lender lender)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_AddLender";

                _command.Parameters.AddWithValue("@CustomerId", lender.CustomerId);
                _command.Parameters.AddWithValue("@BookId", lender.BookId);
                _command.Parameters.AddWithValue("@IssueDate", lender.IssueDate);
                _command.Parameters.AddWithValue("@ReturnDate", lender.ReturnDate);
                _command.Parameters.AddWithValue("@Penalty", lender.Penalty);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public bool DeleteLender(int customerId, int bookId)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_DeleteLender";

                _command.Parameters.AddWithValue("@CustomerId", customerId);
                _command.Parameters.AddWithValue("@BookId", bookId);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public List<Book> GetBookByCustomer(int id)
        {
            List<Book> bookList = new List<Book>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetBooksByCustomer";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    bookList.Add(new Book
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        Author = dr["Author"].ToString()
                    });
                }
                _connection.Close();
            }
            return bookList;
        }

        public Lender GetPenaltyById(int customerId, int bookId)
        {
            Lender lender = new Lender();
            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetPenaltyById";
                _command.Parameters.AddWithValue("@CustomerId", customerId);
                _command.Parameters.AddWithValue("@BookId", bookId);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    lender.Penalty = Convert.ToInt32(dr["Penalty"]);
                }
                _connection.Close();
            }
            return lender;
        }

        public bool UpdatePenalty(int bookId, int customerId, int penalty)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_UpdatePenalty";

                _command.Parameters.AddWithValue("@BookId", bookId);
                _command.Parameters.AddWithValue("@CustomerId", customerId);
                _command.Parameters.AddWithValue("@Penalty", penalty);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

    }
}
