using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Implimentation
{
    public class CartRepository : ICartRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private SqlConnection _conn;
        public CartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("BookStoreAppDB");
            _conn = new SqlConnection(_connectionString);
        }
        public static string connectionString = @"Data Source=LAPTOP-BM4J1NMI;Initial Catalog=bookstore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public static string connectionString = ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);

        public CartItem AddToCart(int userId, CartItem cart)
        {
            using (connection)
            {

                SqlCommand command = new SqlCommand("spAddCart", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@bookId", cart.bookId);
                //command.Parameters.AddWithValue("@CartId", cart.bookId);
                command.Parameters.AddWithValue("@price", cart.price);
                command.Parameters.AddWithValue("@quantity", cart.quantity);
                command.Parameters.AddWithValue("@userId", cart.userId);
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
                return cart;
            }
        }


        public int RemoveFromCart(int userId, int bookId)
        {
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spRemoveFromCart", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@bookId", bookId);
                //command.Parameters.AddWithValue("@userId", userId);

                int isDeleted = command.ExecuteNonQuery();
                connection.Close();
                return isDeleted;
            }

        }

        public List<CartItem> GetCartOfUser(int bookId)
        {
            using (connection) { 
                List<CartItem> CartList = new List<CartItem>();
                connection.Open();
            SqlCommand command = new SqlCommand("spGetCardById", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@bookId", bookId);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    CartList.Add(new CartItem
                    {
                        userId = (int)reader["userId"],
                        bookId = (int)reader["bookId"],
                        quantity = (int)reader["quanity"],
                        cartItem_id = (int)reader["cartItem_id"],
                        price = (int)reader["price"]

                    });
                }
            }
                connection.Close();
            return CartList;
          }

        }
         public List<CartItem> updateCart(int userId, CartItem cart)
        {
            List<CartItem> Carts = new List<CartItem>();
            connection.Open();
            {
                SqlCommand command = new SqlCommand("spupdateCart", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@bookId", cart.bookId);
                //command.Parameters.AddWithValue("@cartItem_id", cart.cartItem_id);
                command.Parameters.AddWithValue("@price", cart.price);
                command.Parameters.AddWithValue("@quantity", cart.quantity);
                command.Parameters.AddWithValue("@userId", cart.userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Carts.Add(new CartItem
                        {
                            bookId = (int)reader["bookId"],
                            //cartItem_id = (int)reader["cartItem_id"],
                            price = (int)reader["price"],
                            quantity = (int)reader["quantity"],
                            userId = (int)reader["userId"]
                        });
                    }
                }
                connection.Close();
                return Carts;
            }
        }



    }
}