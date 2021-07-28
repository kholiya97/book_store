using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer.Database
{
    public class CartItem
    {
        public int cartItem_id { get; set; }
        public int bookId { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int userId { get; set; }
    }
}
