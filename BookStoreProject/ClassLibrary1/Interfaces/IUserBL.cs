using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserRegisterModel AddUser(UserRegisterModel UserReg);
        public Userlogin login(string email, string password);
        public string ForgotPassword(ForgotPasswordModel forgotPass);
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
       
    }
}