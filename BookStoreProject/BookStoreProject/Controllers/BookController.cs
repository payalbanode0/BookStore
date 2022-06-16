using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreProject.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]

    public class BookController : Controller
    {
        private readonly IBookBL BookBL;

        public BookController(IBookBL bookBL)
        {
            this.BookBL = bookBL;
        }

        [HttpPost("AddBook")]
        public IActionResult Book(BookModel book)
        {
            try
            {
                BookModel userData = this.BookBL.AddBook(book);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}