using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        public BookBL(IBookRL userRL)
        {
            this.BookRL = userRL;
        }
        IBookRL BookRL;
        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.BookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBook(int BookId, BookModel updateBook)
        {

            try
            {
                return this.BookRL.UpdateBook(BookId, updateBook);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                return this.BookRL.DeleteBook(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                return this.BookRL.GetBookByBookId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.BookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
