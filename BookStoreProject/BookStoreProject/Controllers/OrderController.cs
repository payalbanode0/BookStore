﻿using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreProject.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL OrderBL;

        public OrderController(IOrderBL orderBL)
        {
            this.OrderBL = orderBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.OrderBL.AddOrder(orderModel, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Order Added SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Order Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("GetAllOrder")]
        public IActionResult GetAllOrder()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.OrderBL.GetAllOrder(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Order List fetched successful ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry! Failed to fetch" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }

}