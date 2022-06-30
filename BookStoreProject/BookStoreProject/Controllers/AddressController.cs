using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("addAddress/{UserId}")]
        public IActionResult AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                var result = this.addressBL.AddAddress(UserId, addressModel);
                if (result.Equals(" Address Added Successfully"))
                {
                    return this.Ok(new { success = true, message = $"Address Added Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPut("updateAddress/{AddressId}")]
        public IActionResult UpdateAddress(int AddressId, AddressModel addressModel)
        {
            try
            {
                var result = this.addressBL.UpdateAddress(AddressId, addressModel);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address updated Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

         [HttpDelete("deletebook/{AddressId}")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var result = this.addressBL.DeleteAddress(AddressId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Address deleted Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}