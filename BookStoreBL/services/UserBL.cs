using BookStoreBLinterface;
using BookStoreCommonLayer.Database;
using BookStoreRL.services;
using BookStoreRLinterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBL.services
{
    public class UserBL : IUserBL
    {
        IUserRL userRl;
        public UserBL(IUserRL userRl)
        {
            this.userRl = userRl;
        }
        public bool AddUser(Users user)
        {
            return this.userRl.AddUser(user);

        }


        public string Login(string email, string password)
        {
            return this.userRl.Login(email, password);
        }
        public bool ForgotPassword(string email)
        {
            try
            {
                return this.userRl.ForgotPassword(email);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}