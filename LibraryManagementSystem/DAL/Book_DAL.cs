using LibraryManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystem.DAL
{
    public class Book_DAL
    {
        public List<Book> GetAllBooks()
        {
            List<Book> bookList = new List<Book>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetAllBooks";

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    bookList.Add(new Book
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        Author = dr["Author"].ToString(),
                        Quantity = Convert.ToInt32(dr["Quantity"])
                    });
                }
                _connection.Close();
            }
            return bookList;
        }

        public bool AddBook(Book book)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_AddBook";

                _command.Parameters.AddWithValue("@ISBN", book.ISBN);
                _command.Parameters.AddWithValue("@Title", book.Title);
                _command.Parameters.AddWithValue("@Author", book.Author);
                _command.Parameters.AddWithValue("@Quantity", book.Quantity);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public Book GetBookById(int id)
        {
            Book book = new Book();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetBookById";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    book.Id = Convert.ToInt32(dr["Id"]);
                    book.ISBN = dr["ISBN"].ToString();
                    book.Title = dr["Title"].ToString();
                    book.Author = dr["Author"].ToString();
                    book.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                _connection.Close();
            }
            return book;
        }

        public bool UpdateBook(Book book)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_UpdateBook";

                _command.Parameters.AddWithValue("@Id", book.Id);
                _command.Parameters.AddWithValue("@ISBN", book.ISBN);
                _command.Parameters.AddWithValue("@Title", book.Title);
                _command.Parameters.AddWithValue("@Author", book.Author);
                _command.Parameters.AddWithValue("@Quantity", book.Quantity);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public bool DeleteBook(int id)
        {
            int rowAffected = 0;

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_DeleteBook";

                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                rowAffected = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return (rowAffected > 0);
        }

        public List<Book> GetBookByISBN(string isbn)
        {
            List<Book> bookList = new List<Book>();

            using (SqlConnection _connection = new SqlConnection(Connection_DAL.ConnectionString()))
            {
                SqlCommand _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetBookByISBN";
                _command.Parameters.AddWithValue("@ISBN", isbn);

                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    bookList.Add(new Book
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        ISBN = dr["ISBN"].ToString(),
                        Title = dr["Title"].ToString(),
                        Author = dr["Author"].ToString(),
                        Quantity = Convert.ToInt32(dr["Quantity"])
                    });
                }
                _connection.Close();
            }
            return bookList;
        }
    }
}
