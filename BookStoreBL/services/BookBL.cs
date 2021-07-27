using BookStoreBLinterface;
using BookStoreCommonLayer.Database;
using BookStoreRLinterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBL.services
{
    public class BookService : IBookService
    {
        private IBookRepository _repository;
        public BookService(IBookRepository repository)
        {
            _repository = repository;

        }
        public BookProduct AddBook(BookProduct book)
        {

            BookProduct result = _repository.AddBook(book);
            return result;
        }

        public int deleteBook(int id)
        {
            int result = _repository.deleteBook(id);
            return result;
        }

        public List<BookProduct> getBooks()
        {
            List<BookProduct> result = _repository.getBooks();
            return result;
        }

        public List<BookProduct> getBookById(int id)
        {
            List<BookProduct> book = _repository.getBookById(id);
            return book;
        }

        public List<BookProduct> updateBook(int id, BookProduct book)
        {
            List<BookProduct> result = _repository.updateBook(id, book);
            return result;

        }
    }
}

