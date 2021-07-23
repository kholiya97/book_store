using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreRL.services
{
    public class UserRL : IUserRL
    {
        public static string connectionString = @"Data Source=LAPTOP-BM4J1NMI;Initial Catalog=bookstore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public static string connectionString = ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);
        public bool AddUser(Users model)
        {
            try
            {
                this.connection.Open();
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.UserRegisterProcedure", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@EmailId", model.EmailId);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
                    command.Parameters.AddWithValue("@role", "Customer");

                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
        //        public string Login(string email, string password)
        //        {
        //            var result = "";
        //            this.connection.Open();
        //            using (this.connection)
        //            {
        //                string query = @"Select * from RegisterUser;";

        //                else
        //                {
        //                    System.Console.WriteLine("No data found");
        //                }


        //            }
        //            if (result == email)
        //            {
        //                var tokenHandler = new JwtSecurityTokenHandler();
        //                var tokenKey = Encoding.ASCII.GetBytes("ilovecodingilovecodingilovecoding");
        //                var tokenDescriptor = new SecurityTokenDescriptor
        //                {
        //                    Subject = new ClaimsIdentity(new Claim[]
        //                    {
        //                   new Claim("Email",email),
        //                        //new Claim("UserID",result.UserId.ToString()),

        //                    }),
        //                    Expires = DateTime.UtcNow.AddDays(7),
        //                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        //                };
        //                var token = tokenHandler.CreateToken(tokenDescriptor);
        //                return tokenHandler.WriteToken(token);
        //            }
        //            else
        //            {
        //                return null;
        //            }

        //        }

        //    }
    }
}




