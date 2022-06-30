using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;

        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        public UserRegisterModel AddUser(UserRegisterModel UserReg)
        {

            try
            {

                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spUserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var passwordToEncript = EncodePasswordToBase64(UserReg.Password);
                cmd.Parameters.AddWithValue("@FullName", UserReg.FullName);
                cmd.Parameters.AddWithValue("@Email", UserReg.Email);
                cmd.Parameters.AddWithValue("@Password", passwordToEncript);

                cmd.Parameters.AddWithValue("@MobileNumber", UserReg.MobileNumber);
                sqlConnection.Open();
                int result = cmd.ExecuteNonQuery();
                sqlConnection.Close();

                if (result != 0)
                {
                    return UserReg;
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
        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }



        public string GetJWTToken(string email, int userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("Email", email),
                    new Claim("UserId",userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public Userlogin login(string email, string password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand com = new SqlCommand("spUserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure //Command type is a class to set as stored procedure
                };
                var passwordToEncript = EncodePasswordToBase64(password);
                com.Parameters.AddWithValue("@Email", email);
                com.Parameters.AddWithValue("@Password", password);

                this.sqlConnection.Open();

                SqlDataReader reader = com.ExecuteReader(); // Execute sqlDataReader to fetching all records
                if (reader.HasRows)  // Checking datareader has rows or not.    
                {
                    int UserId = 0;
                    Userlogin user = new Userlogin();
                    while (reader.Read()) //using while loop for read multiple rows.
                    {
                        user.Email = Convert.ToString(reader["Email"]);
                        user.Password = Convert.ToString(reader["Password"]);
                        UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    this.sqlConnection.Close();
                    user.Token = GetJWTToken(user.Email, UserId);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();  //Always ensuring the closing of the connection
            }

        }

        public string ForgotPassword(ForgotPasswordModel forgotPass)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                SqlCommand cmd = new SqlCommand("spUserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", forgotPass.Email);
                this.sqlConnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                //ExecuteReader method is used to execute a SQL Command or storedprocedure returns a set of rows from the database.

                if (reader.HasRows)//HasRows:-Search there is any row or not
                {
                    int UserId = 0;

                    while (reader.Read()) //using while loop for read multiple rows.
                    {
                        forgotPass.Email = Convert.ToString(reader["Email"]);

                        UserId = Convert.ToInt32(reader["UserId"]);
                    }

                    this.sqlConnection.Close();
                    MessageQueue queue;

                    //Add message to Queue
                    if (MessageQueue.Exists(@".\private$\BookStoreQueue"))
                    {
                        queue = new MessageQueue(@".\private$\BookStoreQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\private$\BookStoreQueue");
                    }
                    Message Mymessage = new Message();
                    Mymessage.Formatter = new BinaryMessageFormatter();
                    Mymessage.Body = GetJWTToken(forgotPass.Email, UserId);
                    EmailServices.SendMail(forgotPass.Email, Mymessage.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    var Token = this.GetJWTToken(forgotPass.Email, UserId);
                    return Token;
                }

                else
                {
                    this.sqlConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();

            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied." + "Queue might be a system queue.");
                }
            }
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookDB"]);
                    SqlCommand com = new SqlCommand("SpUserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
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
    }
    
}
    
   