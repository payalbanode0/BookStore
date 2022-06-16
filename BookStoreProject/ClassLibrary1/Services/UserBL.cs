using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        IUserRL userRL;
        public UserRegisterModel AddUser(UserRegisterModel UserReg)
        {
            try
            {
                return this.userRL.AddUser(UserReg);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Userlogin login(string email, string password)
        {
            try
            {
                return this.userRL.login(email, password);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgotPassword(ForgotPasswordModel forgotPass)
        {
            try
            {
                return this.userRL.ForgotPassword(forgotPass);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}


