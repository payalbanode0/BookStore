using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public string AddBookToCart(AddToCart cartBook)
        {
            try
            {
                return cartRL.AddBookToCart(cartBook);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string DeleteCart(int CartId)
        {
            try
            {
                return cartRL.DeleteCart(CartId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartModel> GetAllBooksinCart(int UserId)
        {
            try
            {
                return cartRL.GetAllBooksinCart(UserId);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    

        public bool UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                return cartRL.UpdateCart(CartId ,BooksQty);

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
