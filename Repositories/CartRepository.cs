using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CartManagementMVC.DBHelper;
using CartManagementMVC.Models;

namespace CartManagementMVC.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SQLHelper _sqlHelper;

        public CartRepository(SQLHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public bool SaveProductInCart(int userId, int productId, int quantity)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@Quantity", quantity)
            };

            int rowsAffected = _sqlHelper.ExecuteNonQuery("SaveProductInCart", parameters);
            return rowsAffected > 0;
        }

        public List<Cart> GetCartListByUserId(int userId)
        {
            List<Cart> cartItems = new List<Cart>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            DataTable dt = _sqlHelper.ExecuteStoredProcedure("GetCartListByUserId", parameters);
            foreach (DataRow row in dt.Rows)
            {
                cartItems.Add(new Cart
                {
                    UserCartId = Convert.ToInt32(row["UserCartId"]),
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductName = row["ProductName"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    Quantity = Convert.ToInt32(row["Quantity"])
                });
            }
            return cartItems;
        }

        public bool RemoveProductFromCart(int userId, int productId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ProductId", productId)
            };

            int rowsAffected = _sqlHelper.ExecuteNonQuery("RemoveProductFromCartByProductIdAndUserId", parameters);
            return rowsAffected > 0;
        }

        public bool UpdateCartQuantity(int userId, int productId, int quantity)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@Quantity", quantity)
            };

            int rowsAffected = _sqlHelper.ExecuteNonQuery("UpdateCartQuantity", parameters);
            return rowsAffected > 0;
        }

    }
}
