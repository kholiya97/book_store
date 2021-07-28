using BookStoreBL.services;
using BookStoreBLinterface;
using BookStoreCommonLayer.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook([FromBody] BookProduct book)
        {
            BookProduct result = _service.AddBook(book);
            return Ok(new  { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });

        }

        [HttpGet("GetAllBooks")]
        public IActionResult getBooks()
        {
            List<BookProduct> result = _service.getBooks();
            return Ok(new  { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });

        }



        [HttpDelete("DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            int result = _service.deleteBook(id);
            return Ok(new  { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });

        }

        [HttpGet("getBookById/{id}")]
        public IActionResult getBookById(int id)
        {
            List<BookProduct> book = _service.getBookById(id);
            return Ok(book);
        }

        [HttpPut("updateBook/{id}")]
        public IActionResult updateBook(int id, [FromBody] BookProduct book)
        {
            List<BookProduct> result = _service.updateBook(id, book);
            return Ok(new response<List<BookProduct>> { StatusCode = (int)HttpStatusCode.OK, Message = "successful", Data = result });

        }

    }
}

