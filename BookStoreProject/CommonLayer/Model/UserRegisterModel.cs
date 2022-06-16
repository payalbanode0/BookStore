using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserRegisterModel
    {
        public int UserId { get; set; }

        //[Required(ErrorMessage = "Full Name required.")]
        public string FullName { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Mobile number is required")]
        public long MobileNumber { get; set; }
    
    }
}
