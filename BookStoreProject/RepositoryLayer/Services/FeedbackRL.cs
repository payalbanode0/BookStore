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
public class FeedbackRL:IFeedbackRL
    {
        private SqlConnection sqlConnection;

        private IConfiguration Configuration { get; }
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId)
      
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookDB"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spAddFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    cmd.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    return feedbackModel;
                }
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
        public List<DisplayFeedback> GetAllFeedback(int bookId)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("spGetFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        List<DisplayFeedback> feedbackModel = new List<DisplayFeedback>();
                        while (reader.Read())
                        {
                            DisplayFeedback getFeedback = new DisplayFeedback();
                            UserRegisterModel user = new UserRegisterModel
                            {
                                FullName = reader["FullName"].ToString()
                            };

                            getFeedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                            getFeedback.Comment = reader["Comment"].ToString();
                            getFeedback.Rating = Convert.ToInt32(reader["Rating"]);
                            getFeedback.BookId = Convert.ToInt32(reader["BookId"]);
                            getFeedback.User = user;
                            feedbackModel.Add(getFeedback);
                        }
                        sqlConnection.Close();
                        return feedbackModel;
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
            finally
            {
                sqlConnection.Close();
            }
        }
    }

       
    }

