using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRL.services
{
    public class UserRL :IUserRL
    {
        public Users AddUser(Users newuser)
        {
            
            return newuser;
        }
        public string Login (string email, string password)
        {
            //var result = _userDbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            //if (result == null)
            //    return null;
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenKey = Encoding.ASCII.GetBytes("ilovecodingilovecodingilovecoding");
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //       new Claim("Email",email),
            //        new Claim("UserID",result.UserId.ToString()),

            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }
    }
}

