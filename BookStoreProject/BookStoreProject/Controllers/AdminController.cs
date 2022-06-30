using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreProject.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminBL AdminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.AdminBL = adminBL;
        }

        [HttpPost("AdminLogin")]
        public IActionResult Admin(AdminResponse adminResponse)
        {
            try
            {
                var result = this.AdminBL.Admin(adminResponse);
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
    }
}
       