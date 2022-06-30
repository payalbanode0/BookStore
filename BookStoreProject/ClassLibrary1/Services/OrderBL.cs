using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL OrderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.OrderRL = orderRL;
        }
        public string AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                return this.OrderRL.AddOrder(orderModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewOrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this.OrderRL.GetAllOrder(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    

}  

          
