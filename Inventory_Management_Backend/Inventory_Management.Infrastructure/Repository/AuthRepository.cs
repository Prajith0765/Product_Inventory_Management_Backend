using Inventory_Management.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Inventory_Management.Application.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        //Validate the user by comparing username and password with the database 
        public async Task<User?> ValidateUserAsync(string username)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_VALIDATE_USER", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PRODUCT_USER_NAME", username);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    ProductUserId = Convert.ToInt64(reader["PRODUCT_USER_ID"]),
                    ProductUserName = reader["PRODUCT_USER_NAME"].ToString(),
                    ProductUserPasswordHash = reader["PRODUCT_USER_PASSWORD_HASH"].ToString(),
                    ProductUserRole = reader["PRODUCT_USER_ROLE"].ToString()
                };

            }
            conn.Close();
            return null;
        }
    }
}
