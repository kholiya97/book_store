using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer.Database
{
    public class BookProduct
    {

        public int bookId { get; set; }
        public string bookName { get; set; }
        public string bookImage { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public long price { get; set; }
        public int addedTocard { get; set; }
    }
}
