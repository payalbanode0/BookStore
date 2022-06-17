using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
      public  string AddBookToCart(AddToCart cartBook);
       public bool UpdateCart(int CartId, int BooksQty);
     public string DeleteCart(int CartId);
      public List<CartModel> GetAllBooksinCart(int UserId);
    }
}
