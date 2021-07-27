using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using Experimental.System.Messaging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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
        public string Login(string email, string password)
        {

            this.connection.Open();
            using (this.connection)
            {

                SqlDataAdapter sda = new SqlDataAdapter("SELECT count(*) FROM Users WHERE EmailId='" + email + "'AND Password='" + password + "'", connection);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes("ilovecodingilovecodingilovecoding");
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                {
                   new Claim("Email",email)
                    //new Claim("UserID",result.UserId.ToString()),

                }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
                else
                {
                    return null;
                }
            }

        }

        public bool ForgotPassword(string email)
        {
            this.connection.Open();
            using (this.connection)
            {

                SqlDataAdapter sda = new SqlDataAdapter("SELECT count(*) FROM Users WHERE EmailId= '" + email + "'", connection);   
                DataTable dt = new DataTable();

                sda.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                      MessageQueue queue;

                   
                    if (MessageQueue.Exists(@".\Private$\BookStoreApplicationQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\BookStoreApplicationQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookStoreApplicationQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = email;
                    MyMessage.Label = "Forget Password Email BookStore Application";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(msg.Body.ToString(), GenerateToken(msg.Body.ToString()));
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                    queue.BeginReceive();
                    queue.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        private string GenerateToken(object p)
        {
            throw new NotImplementedException();
        }

        //// Generate Token
        //public string GenerateToken(string email)
        //{
        //    if (email == null)
        //    {
        //        return null;
        //    }
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //                new Claim("Email",email)
        //        }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials =
        //        new SigningCredentials(
        //            new SymmetricSecurityKey(tokenKey),
        //            SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}


    }


    
    


}






