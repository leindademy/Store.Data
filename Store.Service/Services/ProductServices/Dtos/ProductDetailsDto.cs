namespace Store.Service.Services.ProductService.ProductServices.Dtos
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
