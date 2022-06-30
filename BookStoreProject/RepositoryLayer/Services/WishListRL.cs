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
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);


                SqlCommand cmd = new SqlCommand("AddWishList", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (sqlConnection)
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", wishlistModel.bookId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    int result = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (result != 0)
                    {
                        return wishlistModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spDeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@WishListId", WishListId);

                this.sqlConnection.Open();
                int res = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (res == 0)
                {
                    return "succesful";
                }
                else
                {
                    return "failed";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.sqlConnection.Close();
            }

        }

        public List<ViewWishListModel> GetWishlistByUserid(int UserId)
        {
            try

            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetAllBooksinWishList", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<ViewWishListModel> wishlistmodels = new List<ViewWishListModel>();
                        while (reader.Read())
                        {
                            BookModel bookModel = new BookModel();
                            ViewWishListModel WishlistModel = new ViewWishListModel();
                            bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                            bookModel.BookName = reader["BookName"].ToString();
                            bookModel.AuthorName = reader["AuthorName"].ToString();
                            bookModel.OriginalPrice = Convert.ToInt32(reader["originalPrice"]);
                            bookModel.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            bookModel.BookImage = reader["BookImage"].ToString();
                            WishlistModel.UserId = Convert.ToInt32(reader["UserId"]);
                            WishlistModel.BookId = Convert.ToInt32(reader["BookId"]);
                            WishlistModel.WishlistId = Convert.ToInt32(reader["WishListId"]);
                            WishlistModel.Bookmodel = bookModel;
                            wishlistmodels.Add(WishlistModel);
                        }

                        sqlConnection.Close();
                        return wishlistmodels;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}
    
