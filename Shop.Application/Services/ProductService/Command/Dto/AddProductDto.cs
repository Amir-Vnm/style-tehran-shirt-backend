public class AddProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public double Price { get; set; }
    public int inventory { get; set; }

    public IFormFile Picture { get; set; } // عکس از فرانت
    public string PictureUrl { get; set; } // لینک Cloudinary
}
