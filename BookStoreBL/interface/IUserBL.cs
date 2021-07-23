using BookStoreCommonLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBLinterface
{
   public interface IUserBL
{
        bool AddUser(Users user);
        //string Login(string email, string password);
    }
}

