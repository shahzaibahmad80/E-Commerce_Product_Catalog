using E_Commerce_Product_Catalog.DTOs;
using E_Commerce_Product_Catalog.Models;
using System.Data;
using System.Data.SqlClient;

namespace E_Commerce_Product_Catalog.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbConnectionFactory _db;

        public ProductRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<List<ProductDTO>> GetProductsAsync(ProductFilterDTO filter)
        {
            var list = new List<ProductDTO>();

            using (var conn = _db.CreateConnection())
            using (var cmd = new SqlCommand("sp_GetProducts", (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", (object?)filter.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", (object?)filter.Price ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CategoryId", (object?)filter.CategoryId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Page", filter.Page);
                cmd.Parameters.AddWithValue("@PageSize", filter.PageSize);

                conn.Open();
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    list.Add(new ProductDTO
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Name = reader["Name"].ToString(),
                        SKU = reader["SKU"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Stock = Convert.ToInt32(reader["Stock"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        MainImage = reader["MainImage"]?.ToString()
                    });
                }
            }
            return list;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = new Product { Images = new List<ProductImage>() };

            using (var conn = _db.CreateConnection())
            using (var cmd = new SqlCommand("sp_GetProductById", (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);

                conn.Open();
                var reader = await cmd.ExecuteReaderAsync();

                // product table
                if (await reader.ReadAsync())
                {
                    product.ProductId = id;
                    product.Name = reader["Name"].ToString();
                    product.SKU = reader["SKU"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.Stock = Convert.ToInt32(reader["Stock"]);
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    product.Description = reader["Description"]?.ToString();
                }

                // images table
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        product.Images.Add(new ProductImage
                        {
                            ImageId = Convert.ToInt32(reader["ImageId"]),
                            ImageUrl = reader["ImageUrl"].ToString(),
                        });
                    }
                }
            }

            return product;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            using (var conn = _db.CreateConnection())
            using (var cmd = new SqlCommand("sp_UpdateProduct", (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Description", (object?)product.Description ?? DBNull.Value);

                conn.Open();
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            using (var cmd = new SqlCommand("sp_DeleteProduct", (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);

                conn.Open();
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        // image management
        public async Task<int> AddProductImageAsync(int productId, string imageUrl)
        {
            using (var conn = _db.CreateConnection())
            using (var cmd = new SqlCommand("sp_AddProductImage", (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", productId);
                cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);

                conn.Open();
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task<string> DeleteProductImageAsync(int imageId)
        {
            // return image url to delete the file from disk
            string imageUrl = null;
            using (var conn = _db.CreateConnection())
            using (var cmd = new SqlCommand("sp_DeleteProductImage", (SqlConnection)conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ImageId", imageId);

                // stored proc will return the deleted ImageUrl
                var outParam = new SqlParameter("@DeletedUrl", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                imageUrl = outParam.Value as string;
            }

            return imageUrl;
        }
    }
}