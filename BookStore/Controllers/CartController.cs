using BookStoreBLinterface;
using BookStoreCommonLayer.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TokenAuthentificationFilter]
    public class CartController : Controller
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartItem cart)
        {
            int userId = Convert.ToInt32(HttpContext.Items["userId"]);
            CartItem result = _service.AddTocart(userId, cart);
            return Ok(new  { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
        }

        [HttpDelete("RemoveFromCart")]
        public IActionResult RemoveFromCart(int bookId)
        {
            int userId = Convert.ToInt32(HttpContext.Items["bookId"]);
            int result = _service.RemoveFromCart(userId, bookId);
            return Ok(new  { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
        }

        [HttpGet("GetCartOfUser")]
        public IActionResult GetCartOfUser()
        {
            int bookId = Convert.ToInt32(HttpContext.Items["bookId"]);
            List<CartItem> result = _service.GetCartOfUser(bookId);
            return Ok(new  { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });

        }
        [HttpPut("updateBook/{userId}")]
        public IActionResult updateCart(int userId, [FromBody] CartItem cart)
        {
            List<CartItem> result = _service.updateCart(userId, cart);
            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });
        }

    }
}

