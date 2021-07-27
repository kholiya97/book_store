using BookStoreCommonLayer.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBLinterface
{
    public interface IBookService
    {
        BookProduct AddBook(BookProduct book);
        List<BookProduct> getBooks();
        int deleteBook(int id);
        List<BookProduct> getBookById(int id);
        List<BookProduct> updateBook(int id, BookProduct book);
    }
}
