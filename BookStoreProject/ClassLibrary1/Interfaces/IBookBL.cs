using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
   public interface IBookBL
    {
        public BookModel AddBook(BookModel book);
        public bool UpdateBook(int BookId, BookModel updateBook);
        public bool DeleteBook(int BookId);
        public BookModel GetBookByBookId(int BookId);
    }
}
