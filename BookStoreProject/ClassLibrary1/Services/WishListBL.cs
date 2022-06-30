using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishlistRL;
        public WishListBL(IWishListRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId)
        {
            try
            {
                return this.wishlistRL.AddToWishList(wishlistModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteWishList(int WishListId, int UserId)
        {
            try
            {
                return this.wishlistRL.DeleteWishList(WishListId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ViewWishListModel> GetWishlistByUserid(int UserId)
        {
            try
            {
                return this.wishlistRL.GetWishlistByUserid( UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        }
}
