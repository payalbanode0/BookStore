using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IWishListBL
    {

        public WishListModel AddToWishList(WishListModel wishlistModel, int UserId);

        public string DeleteWishList(int WishListId, int UserId);

        public List<ViewWishListModel> GetWishlistByUserid(int UserId);
    }
}
