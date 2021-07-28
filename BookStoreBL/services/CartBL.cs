using BookStoreBLinterface;
using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Implimentation
{
    public class CartService : ICartService
    {
        private ICartRepository _repository;
        public CartService(ICartRepository repository)
        {
            _repository = repository;

        }
        public CartItem AddTocart(int userId, CartItem cart)
        {
            CartItem result = _repository.AddToCart(userId, cart);
            return result;
        }

        public int RemoveFromCart(int userId, int bookId)
        {

            int result = _repository.RemoveFromCart(userId, bookId);
            return result;
        }

        public List<CartItem> GetCartOfUser(int bookId)
        {
            List<CartItem> result = _repository.GetCartOfUser(bookId);
            return result;
        }
        public List<CartItem> updateCart(int userId, CartItem cart)
        {
            List<CartItem> result = _repository.updateCart(userId, cart);
            return result;

        }
    }
}
