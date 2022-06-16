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


    }
}