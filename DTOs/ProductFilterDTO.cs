namespace E_Commerce_Product_Catalog.DTOs
{
    public class ProductFilterDTO
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }  
        public int? CategoryId { get; set; }

        public int Page { get; set; } = 1;   
        public int PageSize { get; set; } = 10;
    }
}
