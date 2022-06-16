using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;

        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public BookModel AddBook(BookModel book)
        {

            try
            {

                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("SPAddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@TotalRating ", book.TotalRating);
                cmd.Parameters.AddWithValue("@RatingCount ", book.RatingCount);
                cmd.Parameters.AddWithValue("@OriginalPrice ", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@DiscountPrice ", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@BookDetails ", book.BookDetails);
                cmd.Parameters.AddWithValue("@BookImage  ", book.BookImage);
                cmd.Parameters.AddWithValue("@BookQuantity  ", book.BookQuantity);

                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                sqlConnection.Close();

                if (result != 0)
                {
                    return book;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



    }


}
