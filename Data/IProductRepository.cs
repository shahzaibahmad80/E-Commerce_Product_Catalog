using E_Commerce_Product_Catalog.DTOs;
using E_Commerce_Product_Catalog.Models;

namespace E_Commerce_Product_Catalog.Data
{
    public interface IProductRepository
    {
        Task<List<ProductDTO>> GetProductsAsync(ProductFilterDTO filter);
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);

        // image management
        Task<int> AddProductImageAsync(int productId, string imageUrl);
        Task<string> DeleteProductImageAsync(int imageId);
    }
}
