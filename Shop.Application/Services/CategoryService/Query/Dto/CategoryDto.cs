namespace Shop.Application.Services.CategoryService.Query.Dto
{
    public record CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
