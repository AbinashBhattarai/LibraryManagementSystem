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
                        CustomerId = Convert.ToInt32(dr["CustomerId"]),
                        CustomerCode = dr["Code"].ToString(),
                        CustomerName = dr["Name"].ToString(),
                        BookId = Convert.ToInt32(dr["BookId"]),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        IssueDate = Convert.ToDateTime(dr["IssueDate"]),
                        ReturnDate = Convert.ToDateTime(dr["ReturnDate"]),
                        Penalty = Convert.ToInt32(dr["Penalty"]),
                    }); ;
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
                        CustomerName = dr["Name"].ToString(),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        IssueDate = Convert.ToDateTime(dr["IssueDate"]),
                        ReturnDate = Convert.ToDateTime(dr["ReturnDate"]),
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

        public bool DeleteLender(Lender lender)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_DeleteLender";

                _command.Parameters.AddWithValue("@CustomerId", lender.CustomerId);
                _command.Parameters.AddWithValue("@BookId", lender.BookId);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }


        public bool UpdateLender(Lender lender)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_UpdateLender";

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

        public List<Book> GetBooksByLender(int id)
        {
            List<Book> bookList = new List<Book>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetBooksByLender";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    bookList.Add(new Book
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString()
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

        public bool UpdatePenalty(Lender lender)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_UpdatePenalty";

                _command.Parameters.AddWithValue("@BookId", lender.BookId);
                _command.Parameters.AddWithValue("@CustomerId", lender.CustomerId);
                _command.Parameters.AddWithValue("@Penalty", lender.Penalty);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

    }
}
