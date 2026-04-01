using E_Commerce_Product_Catalog.DTOs;
using E_Commerce_Product_Catalog.Models;
using E_Commerce_Product_Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Product_Catalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadProducts(ProductFilterDTO filter)
        {
            var products = await _service.GetProducts(filter);
            return PartialView("_ProductListPartial", products);
        }

        public async Task<IActionResult> ViewProduct(int id)
        {
            var product = await _service.GetProduct(id);
            return PartialView("_ViewProductModal", product);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _service.GetProduct(id);
            return PartialView("_EditProductModal", product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var result = await _service.UpdateProduct(product);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Return only the product id to the confirmation partial (partial expects int)
            return PartialView("_DeleteConfirmation", id);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var result = await _service.DeleteProduct(id);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductImage(int productId, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest();

            var uploads = Path.Combine(_env.WebRootPath, "images", "products");
            Directory.CreateDirectory(uploads);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploads, fileName);
            using (var stream = System.IO.File.Create(filePath)) await file.CopyToAsync(stream);
            var imageUrl = $"/images/products/{fileName}";
            var imageId = await _service.AddProductImage(productId, imageUrl);
            return Json(new { success = imageId > 0, imageId, imageUrl });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(int imageId)
        {
            // get file url from repo via service and delete DB record
            var success = await _service.DeleteProductImage(imageId);
            if (!success) return Json(new { success = false });

            // For now we don't delete the file from disk here.
            return Json(new { success = true });
        }
    }
}
