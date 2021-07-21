using BookStoreCommonLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRLinterface
{
    public interface IUserRL
      {
        Users AddUser(Users user);
        string Login(string email, string password);
    }
}
