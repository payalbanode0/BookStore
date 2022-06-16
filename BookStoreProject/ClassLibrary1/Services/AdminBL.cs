using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL:IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public Adminlogin Admin(AdminResponse adminResponse)
        {
            try
            {
                return this.adminRL.Admin(adminResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
    

