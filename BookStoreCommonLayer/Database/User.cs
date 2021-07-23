using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommonLayer.Database
{
    public class Users
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string role { get; set; }

        //public List<Note> Notes { get; set; }

    }
}
