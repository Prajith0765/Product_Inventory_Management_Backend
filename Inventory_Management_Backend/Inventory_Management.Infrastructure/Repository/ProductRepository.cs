using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using System.Data;
using Inventory_Management.Infrastructure.SQLHelper;
namespace Inventory_Management.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Repository for Adding and Updating Products into the DB Table
        public async Task SaveProductAsync(List<ProductSaveDTO> products, string loggedInUser)
        {
            using SqlConnection conn = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            using SqlCommand cmd = new SqlCommand("SP_SAVE_PRODUCT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var now = DateTime.UtcNow;

            DataTable dt = new DataTable();
            dt.Columns.Add("PRODUCT_ID", typeof(long));
            dt.Columns.Add("PRODUCT_NAME", typeof(string));
            dt.Columns.Add("PRODUCT_CODE", typeof(string));
            dt.Columns.Add("PRODUCT_CATEGORY", typeof(string));
            dt.Columns.Add("PRODUCT_QUANTITY", typeof(int));
            dt.Columns.Add("PRODUCT_EXPIRED_DATE", typeof(DateTime));
            dt.Columns.Add("CREATED_BY", typeof(string));
            dt.Columns.Add("CREATED_AT", typeof(DateTime));
            dt.Columns.Add("MODIFIED_BY", typeof(string));
            dt.Columns.Add("MODIFIED_AT", typeof(DateTime));
            dt.Columns.Add("PRODUCT_ISDELETED", typeof(bool));

            products.ForEach(product =>
            {
                bool isNew = !product.ProductId.HasValue;

                dt.Rows.Add(
                    product.ProductId ?? (object)DBNull.Value,
                    product.ProductName,
                    product.ProductCode,
                    product.ProductCategory,
                    product.ProductQuantity,
                    product.ProductExpiredDate,
                    loggedInUser ?? "SYSTEM",
                    now,
                    isNew ? (object)DBNull.Value : loggedInUser ?? "SYSTEM",
                    isNew ? (object)DBNull.Value : now,
                    false
                );
            });

            SqlParameter tvpParam = new SqlParameter("@PRODUCTS", SqlDbType.Structured)
            {
                TypeName = "TT_PRODUCT_DATA",
                Value = dt
            };

            cmd.Parameters.Add(tvpParam);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();

        }

        //Repository For GetAll Products from the Table
        //public async Task<IEnumerable<ProductResponseDTO>> GetProductsAsync()
        //{


        //    using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //    {
        //        using SqlCommand cmd = new SqlCommand("sp_GetProducts", conn);
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            await conn.OpenAsync();
        //            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
        //            var products = new List<ProductResponseDTO>();
        //            while (await reader.ReadAsync())
        //            {
        //                products.Add(new ProductResponseDTO
        //                {

        //                    ProductId = reader.GetInt32(0),
        //                    ProductName = reader.GetString(1),
        //                    ProductCode = reader.GetString(2),
        //                    Category = reader.GetString(3),
        //                    Quantity = reader.GetInt32(4),
        //                    // get only the date portion (time becomes 00:00:00)
        //                    ExpiredDate = reader.GetDateTime(5).Date
        //                });
        //            }
        //            return products.AsEnumerable();
        //        }
        //    }


        //}
        //Respository for Getting All the Products in a List
        public IList<ProductResponseDTO> GetProducts()
        {
            try
            {
                using var conn = new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));

                using var cmd = new SqlCommand("SP_GETALLPRODUCT", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                var result = DataTableMapper.ToList<ProductResponseDTO>(cmd);
                conn.Close();

                return result;
                    
            }
            catch
            {
                throw;
            }
        }



        //Repository for Get Product By ID  From the Table
        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            {
                using SqlCommand cmd = new SqlCommand("SP_GETPRODUCTBYID", conn);
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PRODUCT_ID", id);
                    await conn.OpenAsync();
                    using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        return new ProductResponseDTO
                        {
                            ProductId = reader.GetInt64(0),
                            ProductName = reader.GetString(1),
                            ProductCode = reader.GetString(2),
                            ProductCategory = reader.GetString(3),
                            ProductQuantity = reader.GetInt32(4),
                            // get only the date portion (time becomes 00:00:00)
                            ProductExpiredDate = reader.GetDateTime(5).Date
                        };
                    }
                    await conn.CloseAsync();

                    return null;
                }
            }
        }

        //Repsoitory for Deleting the Product From the Table
        public async Task DeleteProductAsync(int id, string loggedInUser)
        {
            using SqlConnection conn = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            using SqlCommand cmd = new SqlCommand("SP_DELETEPRODUCT", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", id);
            cmd.Parameters.AddWithValue("@MODIFIED_BY", loggedInUser);
            cmd.Parameters.AddWithValue("@MODIFIED_AT", DateTime.UtcNow);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();
        }



        //Repository for Searching the products by their id, name, code
        public IList<ProductResponseDTO> SearchProducts(string searchText)
        {
            try
            {

                searchText = (searchText ?? string.Empty).Trim();
                using var conn = new SqlConnection(
                    _configuration.GetConnectionString("DefaultConnection"));

                using var cmd = new SqlCommand("SP_SEARCHPRODUCT", conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SEARCH_TEXT", searchText);

                conn.Open();
                
                var result = DataTableMapper.ToList<ProductResponseDTO>(cmd);

                conn.Close();
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
