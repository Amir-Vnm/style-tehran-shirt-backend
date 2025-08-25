namespace Shop.Application.Services.ProductService.Query.Dto
{
    public record ProductDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public double Price { get; set; }
        public int inventory { get; set; }
        public long CategoryId { get; set; }
    }
}
