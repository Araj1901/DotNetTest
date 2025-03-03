using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CartManagementMVC.DBHelper;
using CartManagementMVC.Models;

namespace CartManagementMVC.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLHelper _sqlHelper;

        public ProductRepository(SQLHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public bool SaveProduct(Product product)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductName", product.ProductName),
                new SqlParameter("@ProductDescription", product.ProductDescription ?? (object)DBNull.Value),
                new SqlParameter("@Price", product.Price),
                new SqlParameter("@ProductType", product.ProductType),
                new SqlParameter("@IsActive", product.IsActive),
                new SqlParameter("@CreatedBy", product.CreatedBy)
            };

            int rowsAffected = _sqlHelper.ExecuteNonQuery("SaveProduct", parameters);
            return rowsAffected > 0;
        }

        public List<Product> GetProductList()
        {
            List<Product> products = new List<Product>();
            DataTable dt = _sqlHelper.ExecuteStoredProcedure("GetProductList", null);

            foreach (DataRow row in dt.Rows)
            {
                products.Add(new Product
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductName = row["ProductName"].ToString(),
                    ProductDescription = row["ProductDescription"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    ProductType = row["ProductType"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(row["ModifiedBy"]) : (int?)null,
                    ModifiedDate = row["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(row["ModifiedDate"]) : (DateTime?)null
                });
            }
            return products;
        }

        public Product GetProductById(int productId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductId", productId)
            };

            DataTable dt = _sqlHelper.ExecuteStoredProcedure("GetProductById", parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Product
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductName = row["ProductName"].ToString(),
                    ProductDescription = row["ProductDescription"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    ProductType = row["ProductType"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(row["ModifiedBy"]) : (int?)null,
                    ModifiedDate = row["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(row["ModifiedDate"]) : (DateTime?)null
                };
            }
            return null;
        }
    }
}
