using Microsoft.AspNetCore.Http;

namespace Shop.Application.Services.ProductService.Command.Dto
{
    public record AddProductDto
    {
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public double Price { get; set; }
        public int inventory { get; set; }
        public long CategoryId { get; set; }
    }
}
