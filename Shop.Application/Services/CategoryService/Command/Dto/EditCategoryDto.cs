using Microsoft.AspNetCore.Http;

namespace Shop.Application.Services.CategoryService.Command.Dto
{
    public class EditCategoryDto
    {
        public long Id { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Name { get; set; }
    }
}
