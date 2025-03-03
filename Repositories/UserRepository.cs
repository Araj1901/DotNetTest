using CartManagementMVC.Models;
using CartManagementMVC.DBHelper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CartManagementMVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLHelper _sqlHelper;

        public UserRepository(SQLHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public bool SaveUser(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName)
            };

            int rowsAffected = _sqlHelper.ExecuteNonQuery("SaveUser", parameters);
            return rowsAffected > 0;
        }

        public User ValidateUser(string username, string password)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", username),
                new SqlParameter("@Password", password)
            };

            DataTable dt = _sqlHelper.ExecuteStoredProcedure("ValidateUser", parameters);

            if (dt.Rows.Count > 0)
            {
                return new User
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"]),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    FirstName = dt.Rows[0]["FirstName"].ToString(),
                    LastName = dt.Rows[0]["LastName"].ToString()
                };
            }

            return null;
        }
    }
}
