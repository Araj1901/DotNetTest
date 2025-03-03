using System.Collections.Generic;
using CartManagementMVC.Models;

namespace CartManagementMVC.Repositories
{
    public interface ICartRepository
    {
        bool SaveProductInCart(int userId, int productId, int quantity);
        List<Cart> GetCartListByUserId(int userId);
        bool RemoveProductFromCart(int userId, int productId);
        bool UpdateCartQuantity(int userId, int productId, int quantity);
    }
}
