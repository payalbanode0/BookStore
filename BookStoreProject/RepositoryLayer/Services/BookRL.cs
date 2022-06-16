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

        public bool DeleteBook(int BookId)
        {
            try
            {

                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spDeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookId", BookId);
                
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();


                return true;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spGetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["BookName"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            TotalRating = Convert.ToInt32(reader["TotalRating"]),
                            RatingCount = Convert.ToInt32(reader["RatingCount"]),
                            DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]),
                            BookDetails = reader["BookDetails"].ToString(),
                            BookImage = reader["BookImage"].ToString(),
                            BookQuantity = Convert.ToInt32(reader["BookQuantity"])
                        });
                    }
                    this.sqlConnection.Close();
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
            finally
            {
                this.sqlConnection.Close();
            }
        }

    

        public BookModel GetBookByBookId(int BookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spGetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", BookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                bookModel.BookName = reader["BookName"].ToString();
                bookModel.AuthorName = reader["AuthorName"].ToString();
                bookModel.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                bookModel.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                bookModel.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                bookModel.BookDetails = reader["BookDetails"].ToString();
                bookModel.BookImage = reader["BookImage"].ToString();
                bookModel.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);
                return bookModel;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }



        public bool UpdateBook(int BookId, BookModel updateBook)
        {
            try
            {

                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spUpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookId", BookId);
                cmd.Parameters.AddWithValue("@BookName", updateBook.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", updateBook.AuthorName);
                cmd.Parameters.AddWithValue("@TotalRating ", updateBook.TotalRating);
                cmd.Parameters.AddWithValue("@RatingCount ", updateBook.RatingCount);
                cmd.Parameters.AddWithValue("@OriginalPrice ", updateBook.OriginalPrice);
                cmd.Parameters.AddWithValue("@DiscountPrice ", updateBook.DiscountPrice);
                cmd.Parameters.AddWithValue("@BookDetails ", updateBook.BookDetails);
                cmd.Parameters.AddWithValue("@BookImage  ", updateBook.BookImage);
                cmd.Parameters.AddWithValue("@BookQuantity  ", updateBook.BookQuantity);

                sqlConnection.Open();
                cmd.ExecuteScalar();
                sqlConnection.Close();

              
                return true;
         
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}


