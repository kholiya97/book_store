using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookStoreRL.services
{
    public class BookRepository : IBookRepository
    {
        
        private IConfiguration _configuration;
        private string _connectionString;
        private SqlConnection _conn;
        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("bookstore");
            _conn = new SqlConnection(_connectionString);
        }
        public static string connectionString = @"Data Source=LAPTOP-BM4J1NMI;Initial Catalog=bookstore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public static string connectionString = ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);
        public BookProduct AddBook(BookProduct book)
        {
            using (connection) { 

                ///    SqlCommand command = new SqlCommand("insert into Book(bookName,bookImage,author,description,quantity,price,addedTocard) values(@bookName,@bookImage,@author,@description,@quantity,@price,@addedTocard)");
                SqlCommand command = new SqlCommand("spAddBook", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
                //command.Parameters.AddWithValue("@bookId", 5);
                command.Parameters.AddWithValue("@bookName", book.bookName);
            command.Parameters.AddWithValue("@bookImage", book.bookImage);
            command.Parameters.AddWithValue("@author", book.author);
            command.Parameters.AddWithValue("@description", book.description);
            command.Parameters.AddWithValue("@quantity", book.quantity);
            command.Parameters.AddWithValue("@price", book.price);
            command.Parameters.AddWithValue("@addedTocard", book.addedTocard);
                connection.Open();
            //command.Connection = _conn;
            command.ExecuteNonQuery();
                connection.Close();
            return book;
            }
        }

        public List<BookProduct> getBooks()
        {
            List<BookProduct> books = new List<BookProduct>();
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select bookId,bookName ,bookImage , author, description, quantity,price,addedTocard from Book ", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new BookProduct
                        {
                            bookId = (int)reader["bookId"],
                            bookName = (string)reader["bookName"],
                            bookImage = (string)reader["bookImage"],
                            price = (int)reader["price"],
                            quantity = (int)reader["quantity"],
                            author = (string)reader["author"],
                            description = (string)reader["description"]
                        });
                    }
                }
                connection.Close();

                return books;
            }
        }


        public int deleteBook(int id)
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand("spdeleteBook", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@bookId", id);
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
                return id;
            }

        }

        public List<BookProduct> getBookById(int id)
        {
            List<BookProduct> book = new List<BookProduct>();
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spGetBookById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@bookId", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        book.Add(new BookProduct
                        {
                            bookId = (int)reader["bookId"],
                            bookName = (string)reader["bookName"],
                            bookImage = (string)reader["bookImage"],
                            price = (int)reader["price"],
                            quantity = (int)reader["quantity"],
                            author = (string)reader["author"],
                            description = (string)reader["description"]
                        });
                    }
                }
                connection.Close();
                return book;
            }
        }


        public List<BookProduct> updateBook(int id, BookProduct book)
        {
            List<BookProduct> books = new List<BookProduct>();
            connection.Open();
            {
                SqlCommand command = new SqlCommand("spUpdateBook", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@bookId", id);
                command.Parameters.AddWithValue("@bookName", book.bookName);
                command.Parameters.AddWithValue("@bookImage", book.bookImage);
                command.Parameters.AddWithValue("@author", book.author);
                command.Parameters.AddWithValue("@description", book.description);
                command.Parameters.AddWithValue("@quantity", book.quantity);
                command.Parameters.AddWithValue("@price", book.price);
                command.Parameters.AddWithValue("@addedTocard", book.addedTocard);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new BookProduct
                        {
                            bookId = (int)reader["bookId"],
                            bookName = (string)reader["bookName"],
                            bookImage = (string)reader["bookImage"],
                            price = (int)reader["price"],
                            quantity = (int)reader["quantity"],
                            author = (string)reader["author"],
                            description = (string)reader["description"]
                        });
                    }
                }
                connection.Close();
                return books;
            }
        }

    }

}

