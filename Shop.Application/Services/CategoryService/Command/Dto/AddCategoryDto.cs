using Microsoft.AspNetCore.Http;

namespace Shop.Application.Services.CategoryService.Command.Dto
{
    public record AddCategoryDto
    {
        public IFormFile ImageFile { get; set; }
        public string Name { get; set; }
    }
}
