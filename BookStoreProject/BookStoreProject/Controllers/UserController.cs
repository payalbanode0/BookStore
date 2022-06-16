using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookStoreProject.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL UserBL)
        {
            this.userBL = UserBL;
        }

        [HttpPut("register")]
        public IActionResult AddUser(UserRegisterModel UserReg)
        {
            try
            {
                UserRegisterModel userData = this.userBL.AddUser(UserReg);
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

        [HttpPost("login/{email}/{password}")]
        public IActionResult login(string email, string password)
        {
            try
            {
                var result = this.userBL.login(email, password);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "Login Failed", data = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordModel forgotPass)
        {
            try
            {
                var result = this.userBL.ForgotPassword(forgotPass);
                if (result != null)
                    return this.Ok(new { success = true, message = "forgot password Successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "sorry! Email sending failed", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
               
                var Email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value.ToString();
                if (this.userBL.ResetPassword(Email, newPassword, confirmPassword))
                {
                    return this.Ok(new { Success = true, message = " Password Changed Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Password Change Unsuccessfully " });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
    
