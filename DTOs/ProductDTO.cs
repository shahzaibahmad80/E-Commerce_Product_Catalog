namespace E_Commerce_Product_Catalog.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string SKU { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public decimal Price { get; set; }
        public int Stock{ get; set; }
        public string MainImage { get; set; }
        public List<ProductImageDTO> Images { get; set; } = new List<ProductImageDTO>();
    }
}
