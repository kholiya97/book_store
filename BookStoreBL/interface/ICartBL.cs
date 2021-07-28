using BookStoreCommonLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartService
    {
        CartItem AddTocart(int userId, CartItem cart);
        int RemoveFromCart(int userId, int bookId);
        List<CartItem> GetCartOfUser(int userId);
        List<CartItem> updateCart(int userId, CartItem cart);
    }
}
