using auth.Models;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace auth.Data
{
    public class DapperUserRepository : IUserRepository
    {
        private IDbConnection db;

        public DapperUserRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public User Create(User user)
        {
            var sql = "INSERT INTO User (Name, Email, Password) VALUES(@Name, @Email, @Paswword);";
            var id = db.Query<int>(sql, user).Single();
            user.Id = id;
            return user;

        }

        public User GetByEmail(string email)
        {
             var sql = "SELECT * FROM User  WHERE Email = @Email";
            return db.Query<User>(sql, new { @Email = email }).Single();
        }

        public User GetById(int id)
        {
            var sql = "SELECT * FROM User  WHERE CompanyId = @Id";
            return db.Query<User>(sql, new { @Id = id }).Single();
        }
    }
}
