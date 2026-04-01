using E_Commerce_Product_Catalog.Data;
using E_Commerce_Product_Catalog.DTOs;
using E_Commerce_Product_Catalog.Models;

namespace E_Commerce_Product_Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public Task<List<ProductDTO>> GetProducts(ProductFilterDTO filter)
            => _repo.GetProductsAsync(filter);

        public Task<Product> GetProduct(int id)
            => _repo.GetProductByIdAsync(id);

        public Task<bool> UpdateProduct(Product product)
            => _repo.UpdateProductAsync(product);

        public Task<bool> DeleteProduct(int id)
            => _repo.DeleteProductAsync(id);

        // image management
        public Task<int> AddProductImage(int productId, string imageUrl)
            => _repo.AddProductImageAsync(productId, imageUrl);

        public async Task<bool> DeleteProductImage(int imageId)
        {
            var deletedUrl = await _repo.DeleteProductImageAsync(imageId);
            // service doesn't delete file from disk; controller will handle file deletion when possible
            return !string.IsNullOrEmpty(deletedUrl);
        }
    }
}
