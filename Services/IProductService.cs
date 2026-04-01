using E_Commerce_Product_Catalog.DTOs;
using E_Commerce_Product_Catalog.Models;

namespace E_Commerce_Product_Catalog.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts(ProductFilterDTO filter);
        Task<Product> GetProduct(int id);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<int> AddProductImage(int productId, string imageUrl);
        Task<bool> DeleteProductImage(int imageId);
    }
}
